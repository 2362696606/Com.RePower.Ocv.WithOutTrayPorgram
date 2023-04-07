using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class Battery : ObservableObject
    {
        private string _barCode = string.Empty;

        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode
        {
            get { return _barCode; }
            set { SetProperty(ref _barCode, value); }
        }

        private int _position;

        /// <summary>
        /// 电池位置
        /// </summary>
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        private int? _batteryType;

        /// <summary>
        /// 电池类型
        /// </summary>
        public int? BatteryType
        {
            get { return _batteryType; }
            set { SetProperty(ref _batteryType, value); }
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

        private string? _ocvStationName;

        /// <summary>
        /// Ocv工站名
        /// </summary>
        public string? OcvStationName
        {
            get { return _ocvStationName; }
            set { SetProperty(ref _ocvStationName, value); }
        }

        private double? _volValue;

        /// <summary>
        /// 电压
        /// </summary>
        public double? VolValue
        {
            get { return _volValue; }
            set { SetProperty(ref _volValue, value); }
        }

        private double? _pVolValue;

        /// <summary>
        /// 正极壳电压
        /// </summary>
        public double? PVolValue
        {
            get { return _pVolValue; }
            set { SetProperty(ref _pVolValue, value); }
        }

        private double? _nVolValue;

        /// <summary>
        /// 负极壳体电压
        /// </summary>
        public double? NVolValue
        {
            get { return _nVolValue; }
            set { SetProperty(ref _nVolValue, value); }
        }

        private double? _res;

        /// <summary>
        /// 内阻
        /// </summary>
        public virtual double? Res
        {
            get { return _res; }
            set { SetProperty(ref _res, value); }
        }

        private double? _temp;

        /// <summary>
        /// 温度
        /// </summary>
        public double? Temp
        {
            get { return _temp; }
            set { SetProperty(ref _temp, value); }
        }

        private double? _pTemp;

        /// <summary>
        /// 正极温度
        /// </summary>
        public double? PTemp
        {
            get { return _pTemp; }
            set { SetProperty(ref _pTemp, value); }
        }

        private double? _nTemp;

        /// <summary>
        /// 负极温度
        /// </summary>
        public double? NTemp
        {
            get { return _nTemp; }
            set { SetProperty(ref _nTemp, value); }
        }

        private double? _kValue1;

        /// <summary>
        /// K值1
        /// </summary>
        public double? KValue1
        {
            get { return _kValue1; }
            set { SetProperty(ref _kValue1, value); }
        }

        private double? _kValue2;

        /// <summary>
        /// K值2
        /// </summary>
        public double? KValue2
        {
            get { return _kValue2; }
            set { SetProperty(ref _kValue2, value); }
        }

        private double? _kValue3;

        /// <summary>
        /// K值3
        /// </summary>
        public double? KValue3
        {
            get { return _kValue3; }
            set { SetProperty(ref _kValue3, value); }
        }

        private double? _kValue4;

        /// <summary>
        /// K值4
        /// </summary>
        public double? KValue4
        {
            get { return _kValue4; }
            set { SetProperty(ref _kValue4, value); }
        }

        private double? _kValue5;

        /// <summary>
        /// K值5
        /// </summary>
        public double? KValue5
        {
            get { return _kValue5; }
            set { SetProperty(ref _kValue5, value); }
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

        private bool _isTested;

        /// <summary>
        /// 是否已经测试
        /// </summary>
        public bool IsTested
        {
            get { return _isTested; }
            set { SetProperty(ref _isTested, value); }
        }

        private bool _isExsit = true;

        /// <summary>
        /// 电芯是否存在
        /// </summary>
        public bool IsExsit
        {
            get { return _isExsit; }
            set { SetProperty(ref _isExsit, value); }
        }

        private bool _isReal = true;

        /// <summary>
        /// 电芯是否为真
        /// </summary>
        public bool IsReal
        {
            get { return _isReal; }
            set { SetProperty(ref _isReal, value); }
        }

        private DateTime _testTime = DateTime.Now;

        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime
        {
            get { return _testTime; }
            set { SetProperty(ref _testTime, value); }
        }

        private long? _taskCode;

        /// <summary>
        /// 任务号
        /// </summary>
        public long? TaskCode
        {
            get { return _taskCode; }
            set { SetProperty(ref _taskCode, value); }
        }

        /// <summary>
        /// 保留int类型1
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt1;

        /// <summary>
        /// 保留int类型2
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt2;

        /// <summary>
        /// 保留int类型3
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt3;

        /// <summary>
        /// 保留int类型4
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt4;

        /// <summary>
        /// 保留int类型5
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt5;

        /// <summary>
        /// 保留double类型1
        /// </summary>
        [ObservableProperty]
        private double? _reserveValue1;

        /// <summary>
        /// 保留double类型2
        /// </summary>
        [ObservableProperty]
        private double? _reserveValue2;

        /// <summary>
        /// 保留double类型3
        /// </summary>
        [ObservableProperty]
        private double? _reserveValue3;

        /// <summary>
        /// 保留double类型4
        /// </summary>
        [ObservableProperty]
        private double? _reserveValue4;

        /// <summary>
        /// 保留double类型5
        /// </summary>
        [ObservableProperty]
        private double? _reserveValue5;

        /// <summary>
        /// 保留string类型1
        /// </summary>
        [ObservableProperty]
        private string? _reserveText1;

        /// <summary>
        /// 保留string类型2
        /// </summary>
        [ObservableProperty]
        private string? _reserveText2;

        /// <summary>
        /// 保留string类型3
        /// </summary>
        [ObservableProperty]
        private string? _reserveText3;

        /// <summary>
        /// 保留string类型4
        /// </summary>
        [ObservableProperty]
        private string? _reserveText4;

        /// <summary>
        /// 保留string类型5
        /// </summary>
        [ObservableProperty]
        private string? _reserveText5;

        /// <summary>
        /// 保留时间1
        /// </summary>
        [ObservableProperty]
        private DateTime? _reserveTime1;

        /// <summary>
        /// 保留时间2
        /// </summary>
        [ObservableProperty]
        private DateTime? _reserveTime2;

        /// <summary>
        /// 保留时间3
        /// </summary>
        [ObservableProperty]
        private DateTime? _reserveTime3;

        /// <summary>
        /// 保留时间4
        /// </summary>
        [ObservableProperty]
        private DateTime? _reserveTime4;

        /// <summary>
        /// 保留时间5
        /// </summary>
        [ObservableProperty]
        private DateTime? _reserveTime5;
    }
}