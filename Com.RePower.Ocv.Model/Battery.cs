using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model
{
    public partial class Battery:ObservableObject
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        [ObservableProperty]
        private string _barCode = string.Empty;
        /// <summary>
        /// 电池位置
        /// </summary>
        [ObservableProperty]
        private int _position;
        /// <summary>
        /// 电压
        /// </summary>
        [ObservableProperty]
        private double _volValue;
        /// <summary>
        /// 正极壳电压
        /// </summary>
        [ObservableProperty]
        private double _pVolValue;
        /// <summary>
        /// 负极壳体电压
        /// </summary>
        [ObservableProperty]
        private double _nVolValue;
        /// <summary>
        /// 内阻
        /// </summary>
        [ObservableProperty]
        private double _res;
        /// <summary>
        /// 温度
        /// </summary>
        [ObservableProperty]
        private double _temp;
        /// <summary>
        /// 正极温度
        /// </summary>
        [ObservableProperty]
        private double _pTemp;
        /// <summary>
        /// 负极温度
        /// </summary>
        [ObservableProperty]
        private double _nTemp;
        /// <summary>
        /// K值
        /// </summary>
        [ObservableProperty]
        private double _kValue;
        /// <summary>
        /// 电池类型
        /// </summary>
        [ObservableProperty]
        private int _batteryType;
        /// <summary>
        /// 是否已经测试
        /// </summary>
        [ObservableProperty]
        private bool _isTested = false;
        /// <summary>
        /// 测试时间
        /// </summary>
        [ObservableProperty]
        private DateTime _testTime;
    }
}