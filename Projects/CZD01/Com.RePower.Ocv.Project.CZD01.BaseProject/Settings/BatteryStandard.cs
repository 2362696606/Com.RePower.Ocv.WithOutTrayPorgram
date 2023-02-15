using Com.RePower.Ocv.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class BatteryStandard:Model.Settings.BatteryStandard
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


    }
}
