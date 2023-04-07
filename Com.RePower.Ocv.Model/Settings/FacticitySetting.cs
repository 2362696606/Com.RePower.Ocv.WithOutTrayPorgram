using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Settings
{
    public class FacticitySetting : ObservableObject
    {
        private bool _isRealPlc;
        private bool _isRealOhm;
        private bool _isRealDmm;
        private bool _isRealMtvs;
        private bool _isRealSwitchBoard;
        private bool _isRealMes;
        private bool _isRealWms;
        private bool _isRealTemperatureSensor;

        /// <summary>
        /// 是否是真实Plc
        /// </summary>
        public bool IsRealPlc
        {
            get { return _isRealPlc; }
            set { SetProperty(ref _isRealPlc, value); }
        }

        /// <summary>
        /// 是否是真实内阻仪
        /// </summary>
        public bool IsRealOhm
        {
            get { return _isRealOhm; }
            set { SetProperty(ref _isRealOhm, value); }
        }

        /// <summary>
        /// 是否是真实万用表
        /// </summary>
        public bool IsRealDmm
        {
            get { return _isRealDmm; }
            set { SetProperty(ref _isRealDmm, value); }
        }

        /// <summary>
        /// 是否是真实Mtvs
        /// </summary>
        public bool IsRealMtvs
        {
            get { return _isRealMtvs; }
            set { SetProperty(ref _isRealMtvs, value); }
        }

        /// <summary>
        /// 是否是真实切换板
        /// </summary>
        public bool IsRealSwitchBoard
        {
            get { return _isRealSwitchBoard; }
            set { SetProperty(ref _isRealSwitchBoard, value); }
        }

        /// <summary>
        /// 是否是真实调度
        /// </summary>
        public bool IsRealWms
        {
            get { return _isRealWms; }
            set { SetProperty(ref _isRealWms, value); }
        }

        /// <summary>
        /// 是否是真实Mes
        /// </summary>
        public bool IsRealMes
        {
            get { return _isRealMes; }
            set { SetProperty(ref _isRealMes, value); }
        }

        /// <summary>
        /// 是否是真实温度传感器
        /// </summary>
        public bool IsRealTemperatureSensor
        {
            get { return _isRealTemperatureSensor; }
            set { SetProperty(ref _isRealTemperatureSensor, value); }
        }
    }
}