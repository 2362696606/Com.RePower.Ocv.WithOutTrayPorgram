using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class BatteryStandard:Model.Settings.BatteryStandard,ISettingSaveChanged
    {

        private double _maxVolDifference;
        /// <summary>
        /// 压差最大值
        /// </summary>
        [SettingName("压差最大值")]
        public double MaxVolDifference
        {
            get { return _maxVolDifference; }
            set { SetProperty(ref _maxVolDifference, value); }
        }
        private double _minVolDifference;
        /// <summary>
        /// 压差最小值
        /// </summary>
        public double MinVolDifference
        {
            get { return _minVolDifference; }
            set { SetProperty(ref _minVolDifference, value); }
        }
        private double _xValue;
        /// <summary>
        /// 单托盘Ng-X值
        /// </summary>
        [SettingName("单托盘Ng-X值")]
        public double XValue
        {
            get { return _xValue; }
            set { SetProperty(ref _xValue, value); }
        }


        /// <summary>
        /// 用于保存
        /// </summary>
        [IgnorSetting]
        [JsonIgnore]
        public Func<string, OperateResult>? SaveEvent { get; set; }
        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke("BatteryStandard") ?? OperateResult.CreateFailedResult("保存委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
