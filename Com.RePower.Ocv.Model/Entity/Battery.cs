using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Entity
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
        /// 电池类型
        /// </summary>
        [ObservableProperty]
        private int? _batteryType;
        /// <summary>
        /// Ocv类型
        /// </summary>
        [ObservableProperty]
        private string? _ocvType;
        /// <summary>
        /// Ocv工站名
        /// </summary>
        [ObservableProperty]
        private string? _ocvStationName;
        /// <summary>
        /// 电压
        /// </summary>
        [ObservableProperty]
        private double? _volValue;
        /// <summary>
        /// 正极壳电压
        /// </summary>
        [ObservableProperty]
        private double? _pVolValue;
        /// <summary>
        /// 负极壳体电压
        /// </summary>
        [ObservableProperty]
        private double? _nVolValue;
        /// <summary>
        /// 内阻
        /// </summary>
        [ObservableProperty]
        private double? _res;
        /// <summary>
        /// 温度
        /// </summary>
        [ObservableProperty]
        private double? _temp;
        /// <summary>
        /// 正极温度
        /// </summary>
        [ObservableProperty]
        private double? _pTemp;
        /// <summary>
        /// 负极温度
        /// </summary>
        [ObservableProperty]
        private double? _nTemp;
        /// <summary>
        /// K值1
        /// </summary>
        [ObservableProperty]
        private double? _kValue1;
        /// <summary>
        /// K值2
        /// </summary>
        [ObservableProperty]
        private double? _kValue2;
        /// <summary>
        /// K值3
        /// </summary>
        [ObservableProperty]
        private double? _kValue3;
        /// <summary>
        /// K值4
        /// </summary>
        [ObservableProperty]
        private double? _kValue4;
        /// <summary>
        /// 是否已经测试
        /// </summary>
        [ObservableProperty]
        private bool _isTested = false;
        /// <summary>
        /// 电芯是否存在
        /// </summary>
        [ObservableProperty]
        private bool _isExsit = true;
        /// <summary>
        /// 电芯是否为真
        /// </summary>
        [ObservableProperty]
        private bool _isReal = true;
        /// <summary>
        /// 测试时间
        /// </summary>
        [ObservableProperty]
        private DateTime _testTime = DateTime.Now;
        /// <summary>
        /// 保留int类型1
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt1 ;
        /// <summary>
        /// 保留int类型2
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt2 ;
        /// <summary>
        /// 保留int类型3
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt3 ;
        /// <summary>
        /// 保留int类型4
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt4 ;
        /// <summary>
        /// 保留int类型5
        /// </summary>
        [ObservableProperty]
        private int? _reserveInt5 ;
        /// <summary>
        /// K值5
        /// </summary>
        [ObservableProperty]
        private double? _kValue5;
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