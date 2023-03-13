using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Mapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Com.RePower.Ocv.Ui.UiBase.ViewModels
{
    public class DataSearchViewModel:ObservableObject
    {
        public DataSearchViewModel(LocalTestResultDbContext dbContext)
        {
            _dbContext = dbContext;
            this.DoSearchCommand = new AsyncRelayCommand(DoSearch);
            this.DoExportCommand = new AsyncRelayCommand(DoExport);
            DoSearchCommand.Execute(this);

            var configuration = new MapperConfiguration(cfg => {
                //cfg.CreateMap<Foo, Bar>();
                cfg.CreateMap<NgInfoDto, BatteryToExcelDto>().IncludeMembers(x => x.Battery);
                cfg.CreateMap<BatteryDto, BatteryToExcelDto>();
            });
            Mapper = configuration.CreateMapper();//或者var mapper=new Mapper(configuration);
            MessageQueue = new SnackbarMessageQueue();
        }
        public IMapper Mapper { get; }

        public SnackbarMessageQueue MessageQueue { get; private set; }

        private LocalTestResultDbContext _dbContext;

        public virtual LocalTestResultDbContext DbContext
        {
            get { return _dbContext; }
            set { SetProperty(ref _dbContext, value); }
        }
        private List<Ocv.Model.Dto.NgInfoDto>? _itemsSource;
        /// <summary>
        /// 数据源
        /// </summary>
        public virtual List<Ocv.Model.Dto.NgInfoDto>? ItemsSource
        {
            get { return _itemsSource; }
            set { SetProperty(ref _itemsSource, value); }
        }
        private DateTime? _startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }
        private DateTime? _endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }
        private string? _trayCode;
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string? TrayCode
        {
            get { return _trayCode; }
            set { SetProperty(ref _trayCode, value); }
        }
        private string? _barCode;
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string? BarCode
        {
            get { return _barCode; }
            set { SetProperty(ref _barCode, value); }
        }
        private string? _ocvType;
        /// <summary>
        /// Ocv类型
        /// </summary>
        public string? OcvType
        {
            get { return _ocvType; }
            set { SetProperty(ref _ocvType, value); }
        }
        private int _maxPageCount = 1;

        public int MaxPageCount
        {
            get { return _maxPageCount; }
            set { SetProperty(ref _maxPageCount, value); }
        }
        private int _dataCountPerPage = 20;

        public int DataCountPerPage
        {
            get { return _dataCountPerPage; }
            set { SetProperty(ref _dataCountPerPage, value); }
        }
        private int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex; }
            set 
            {
                if (SetProperty(ref _pageIndex, value))
                {
                    if (!DoSearchCommand.IsRunning)
                    {
                        DoSearchCommand.Execute(this); 
                    }
                } 
            }
        }

        public IAsyncRelayCommand DoSearchCommand { get; }
        public IAsyncRelayCommand DoExportCommand { get; }

        private Task DoSearch()
        {
            return Task.Factory.StartNew(() =>
            {
                var count = DbContext.NgInfos.Where(x =>
                ((string.IsNullOrEmpty(_trayCode) ? true : x.Battery.TrayCode!.Contains(_trayCode))
                                    && (string.IsNullOrEmpty(_barCode) ? true : x.Battery.BarCode!.Contains(_barCode))
                                    && (string.IsNullOrEmpty(_ocvType) ? true : x.Battery.OcvType!.Contains(_ocvType))
                                    && ((_startTime == null) ? true : (x.Battery.TestTime >= _startTime))
                                    && ((_endTime == null) ? true : (x.Battery.TestTime <= _endTime))))
                                    .OrderBy(x => x.Id)?.Count() ?? 0;
                MaxPageCount = (count / _dataCountPerPage) + 1;

                ItemsSource = DbContext.NgInfos.Where(x =>
                ((string.IsNullOrEmpty(_trayCode) ? true : x.Battery.TrayCode!.Contains(_trayCode))
                                    && (string.IsNullOrEmpty(_barCode) ? true : x.Battery.BarCode!.Contains(_barCode))
                                    && (string.IsNullOrEmpty(_ocvType) ? true : x.Battery.OcvType!.Contains(_ocvType))
                                    && ((_startTime == null) ? true : (x.Battery.TestTime >= _startTime))
                                    && ((_endTime == null) ? true : (x.Battery.TestTime <= _endTime))))
                                .Include(x => x.Battery)
                                .OrderBy(x => x.Battery.Id)
                                .Skip((_pageIndex - 1) * _dataCountPerPage)
                                .Take(_dataCountPerPage).ToList();
            });
        }
        private Task DoExport()
        {
            return Task.Factory.StartNew(() =>
            {
                List<BatteryToExcelDto> dtos = Mapper.Map<List<BatteryToExcelDto>>(ItemsSource);
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "xlsx|*.xlsx|All files|*.*";
                //saveFileDialog.InitialDirectory = "./";

                try
                {
                    var openResult = saveFileDialog.ShowDialog();
                    if (openResult ?? false)
                    {
                        string fullPath = saveFileDialog.FileName;
                        string? path = Path.GetDirectoryName(fullPath);
                        string? fileName = Path.GetFileName(fullPath);
                        if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        new Npoi.Mapper.Mapper().Save(fullPath, dtos, "sheet1");
                        MessageQueue.Enqueue("导出成功");
                        return;
                    }
                }
                catch (Exception e)
                {
                    MessageQueue.Enqueue($"导出失败:{e}");
                }

                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Image1|*.bmp;*.png|Image2|*.jepg";
                //saveFileDialog.InitialDirectory = "C:\\Users\\Desktop\\Image"; //设置初始目录
                //if (saveFileDialog.ShowDialog() && Image = null)
                //{
                //    string name = saveFileDialog.FileName; //获取选择的文件，或者自定义的文件名的全路径。
                //    Image.ImWrite(name);    //将文件进行保存，这里IO流或者其它保存文件方法都可以
                //}
            });
        }
    }
    //public class NgInfoToExcelDto
    //{
    //    public long Id { get; set; }
    //    /// <summary>
    //    /// 对应电池
    //    /// </summary>
    //    public virtual BatteryDto Battery { get; set; } = new BatteryDto();
    //    /// <summary>
    //    /// mg描述
    //    /// </summary>
    //    public string? NgDescription { get; set; }
    //    /// <summary>
    //    /// 是否ng
    //    /// </summary>
    //    public bool IsNg { get; set; } = false;
    //    /// <summary>
    //    /// ng类型
    //    /// </summary>
    //    public int? NgType { get; set; }
    //}
    public class BatteryToExcelDto
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 电池位置
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        public int? BatteryType { get; set; }
        /// <summary>
        /// Ocv类型
        /// </summary>
        public string? OcvType { get; set; }
        /// <summary>
        /// Ocv工站名
        /// </summary>
        public string? OcvStationName { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public double? VolValue { get; set; }
        /// <summary>
        /// 正极壳电压
        /// </summary>
        public double? PVolValue { get; set; }
        /// <summary>
        /// 负极壳电压
        /// </summary>
        public double? NVolValue { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public double? Res { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public double? Temp { get; set; }
        /// <summary>
        /// 正极温度
        /// </summary>
        public double? PTemp { get; set; }
        /// <summary>
        /// 负极温度
        /// </summary>
        public double? NTemp { get; set; }
        /// <summary>
        /// mg描述
        /// </summary>
        public string? NgDescription { get; set; }
        /// <summary>
        /// 是否ng
        /// </summary>
        public bool IsNg { get; set; } = false;
        ///// <summary>
        ///// K值1
        ///// </summary>
        //public double? KValue1 { get; set; }
        ///// <summary>
        ///// K值2
        ///// </summary>
        //public double? KValue2 { get; set; }
        ///// <summary>
        ///// K值3
        ///// </summary>
        //public double? KValue3 { get; set; }
        ///// <summary>
        ///// K值4
        ///// </summary>
        //public double? KValue4 { get; set; }
        ///// <summary>
        ///// K值5
        ///// </summary>
        //public double? KValue5 { get; set; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string? TrayCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
        /// <summary>
        /// 任务号
        /// </summary>
        public long? TaskCode { get; set; }
    }
}
