using Com.RePower.Ocv.Model.Entity;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Settings
{
    public class CalibrationSetting : ObservableObject
    {
        private bool _isUseCalibration;

        /// <summary>
        /// 是否启用校准
        /// </summary>
        public bool IsUseCalibration
        {
            get { return _isUseCalibration; }
            set { SetProperty(ref _isUseCalibration, value); }
        }

        private bool _isUseAutoValue;

        /// <summary>
        /// 使用自动校准值
        /// </summary>
        public bool IsUseAutoValue
        {
            get { return _isUseAutoValue; }
            set { SetProperty(ref _isUseAutoValue, value); }
        }

        private List<CalibrationValue>? _calibrationValues;

        /// <summary>
        /// 校准值列表
        /// </summary>
        public List<CalibrationValue>? CalibrationValues
        {
            get { return _calibrationValues; }
            set { SetProperty(ref _calibrationValues, value); }
        }
    }
}