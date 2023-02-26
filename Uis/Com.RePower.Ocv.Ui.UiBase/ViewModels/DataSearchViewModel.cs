using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
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
            DoSearchCommand.Execute(this);
        }
        private LocalTestResultDbContext _dbContext;

        public virtual LocalTestResultDbContext DbContext
        {
            get { return _dbContext; }
            set { SetProperty(ref _dbContext, value); }
        }
        private List<Ocv.Model.Dto.BatteryDto>? _itemsSource;
        /// <summary>
        /// 数据源
        /// </summary>
        public virtual List<Ocv.Model.Dto.BatteryDto>? ItemsSource
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

        private Task DoSearch()
        {
            return Task.Factory.StartNew(() =>
            {
                var count = DbContext.Batterys.Where(x =>
                ((string.IsNullOrEmpty(_trayCode) ? true : x.TrayCode!.Contains(_trayCode))
                                    && (string.IsNullOrEmpty(_barCode) ? true : x.BarCode!.Contains(_barCode))
                                    && ((_startTime == null) ? true : (x.TestTime >= _startTime))
                                    && ((_endTime == null) ? true : (x.TestTime <= _endTime))))
                                    .OrderBy(x => x.Id)?.Count() ?? 0;
                MaxPageCount = (count / _dataCountPerPage) + 1;

                ItemsSource = DbContext.Batterys.Where(x =>
                ((string.IsNullOrEmpty(_trayCode) ? true : x.TrayCode!.Contains(_trayCode))
                                    && (string.IsNullOrEmpty(_barCode) ? true : x.BarCode!.Contains(_barCode))
                                    && ((_startTime == null) ? true : (x.TestTime >= _startTime))
                                    && ((_endTime == null) ? true : (x.TestTime <= _endTime))))
                                .OrderBy(x => x.Id)
                                .Skip((_pageIndex - 1) * _dataCountPerPage)
                                .Take(_dataCountPerPage).ToList();
            });
        }
    }
}
