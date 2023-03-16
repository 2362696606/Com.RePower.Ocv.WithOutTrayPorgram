using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Settings
{
    public class AcirOption:ObservableObject, ISettingSaveChanged
    {

        public List<TempFit> TempFits { get; set; } = new List<TempFit>();
        private decimal? _nominalTemp;
        /// <summary>
        /// 标称温度
        /// </summary>
        public decimal? NominalTemp
        {
            get { return _nominalTemp; }
            set { SetProperty(ref _nominalTemp, value); }
        }
        private decimal? _currentTrayRequirements;
        /// <summary>
        /// 同盘管控要求
        /// </summary>
        public decimal? CurrentTrayRequirements
        {
            get { return _currentTrayRequirements; }
            set { SetProperty(ref _currentTrayRequirements, value); }
        }
        private decimal? _dcirMax;
        /// <summary>
        /// Dcir最大值
        /// </summary>
        public decimal? DcirMax
        {
            get { return _dcirMax; }
            set { SetProperty(ref _dcirMax, value); }
        }
        private decimal? _dcirMin;
        /// <summary>
        /// Dcir最小值
        /// </summary>
        public decimal? DcirMin
        {
            get { return _dcirMin; }
            set { SetProperty(ref _dcirMin, value); }
        }
        private bool? _isAcirEnable;
        /// <summary>
        /// 温度拟合是否启用
        /// </summary>
        public bool? IsAcirEnable
        {
            get { return _isAcirEnable; }
            set { SetProperty(ref _isAcirEnable, value); }
        }
        private bool? _isDcirEnable;
        /// <summary>
        /// 同盘管控是否启用
        /// </summary>
        public bool? IsDcirEnable
        {
            get { return _isDcirEnable; }
            set { SetProperty(ref _isDcirEnable, value); }
        }



        /// <summary>
        /// 用于保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string, OperateResult>? SaveEvent { get; set; }
        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke("AcirOption") ?? OperateResult.CreateFailedResult("保存设置委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }

    }
    public class TempFit:ObservableObject
    {
        private decimal? _minTemp;
        /// <summary>
        /// 区间最小值
        /// </summary>
        public decimal? MinTemp
        {
            get { return _minTemp; }
            set { SetProperty(ref _minTemp, value); }
        }
        private decimal? _maxTemp;
        /// <summary>
        /// 区间最大值
        /// </summary>
        public decimal? MaxTemp
        {
            get { return _maxTemp; }
            set { SetProperty(ref _maxTemp, value); }
        }
        private decimal? _fitFactor;
        /// <summary>
        /// 拟合系数
        /// </summary>
        public decimal? FitFactor
        {
            get { return _fitFactor; }
            set { SetProperty(ref _fitFactor, value); }
        }

    }
}
