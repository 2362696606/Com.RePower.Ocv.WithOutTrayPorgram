using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers
{
    public class SettingManager : ObservableObject
    {
        private readonly string _defaultOcvTypeSettingName = "DefaultOcvType";
        private readonly string _facticitySettingName = "FacticitySetting";
        private readonly string _testOptionSettingName = "TestOptionSetting";
        private readonly string _batteryStandardSettingName = "BatteryStandardSetting";
        private readonly string _wmsSettingName = "WmsSetting";
        private readonly string _testOrderSettingName = "TestOrder";
        private readonly string _plcSettingName = "PlcSetting";
        private readonly string _dmmSettingName = "DmmSetting";
        private readonly string _ohmSettingName = "OhmSetting";
        private readonly string _switchBoardSettingName = "SwitchBoardSetting";
        private readonly string _temperatureSensorSettingName = "TemperatureSetting";
        private readonly string _plcAddressCacheSettingName = "PlcAddressCache";
        private readonly string _otherSettingName = "OtherSetting";
        private readonly string _sceneConnectStringSettingName = "OcvConnectString";
        private readonly string _calibrationSettingName = "CalibrationSetting";

        private readonly IMapper _mapper;

        private static readonly Lazy<SettingManager> _instance =
           new Lazy<SettingManager>(() => new SettingManager());

        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static SettingManager Instance
        { get { return _instance.Value; } }

        private SettingManager()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SettingProfile>();
                cfg.AddProfile<SettingForCzd01Profile>();
            });
            this._mapper = configuration.CreateMapper();
            using OcvSettingDbContext context = new OcvSettingDbContext();

            #region 初始化OcvType

            var ocvTypeJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _defaultOcvTypeSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(ocvTypeJStr))
            {
                this._ocvType = Enum.Parse<OcvTypeEnum>(ocvTypeJStr);
            }

            #endregion 初始化OcvType

            #region 初始化设备真实性信息

            var facticityJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _facticitySettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(facticityJStr))
            {
                var dto = JsonConvert.DeserializeObject<FacticitySettingDto>(facticityJStr);
                this._facticitySetting = _mapper.Map<FacticitySetting>(dto);
            }

            #endregion 初始化设备真实性信息

            #region 初始化TestOption

            var testOptionJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _testOptionSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionJStr))
            {
                var dto = JsonConvert.DeserializeObject<Settings.Dtos.TestOptionDto>(testOptionJStr);
                this._testOption = _mapper.Map<Settings.TestOption>(dto);
                _testOption.SaveEvent = SaveChange;
            }

            #endregion 初始化TestOption

            #region 初始化BatteryStandard

            var batteryStandardJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _batteryStandardSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(batteryStandardJStr))
            {
                var dto = JsonConvert.DeserializeObject<Settings.Dtos.BatteryStandardSettingDto>(batteryStandardJStr);
                this._batteryStandard = _mapper.Map<Settings.BatteryStandard>(dto);
                _batteryStandard.SaveEvent = SaveChange;
            }

            #endregion 初始化BatteryStandard

            #region 初始化WmsSetting

            var wmsJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _wmsSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(wmsJStr))
            {
                var dto = JsonConvert.DeserializeObject<WmsSettingDto>(wmsJStr);
                this._wmsSetting = _mapper.Map<WmsSetting>(dto);
            }

            #endregion 初始化WmsSetting

            #region 初始化TestOrder

            var testOrderJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _testOrderSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(testOrderJStr))
            {
                this._testOrder = JsonConvert.DeserializeObject<List<List<int>>>(testOrderJStr);
            }

            #endregion 初始化TestOrder

            #region 初始化OtherSetting

            var otherSettingJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _otherSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(otherSettingJStr))
            {
                var dto = JsonConvert.DeserializeObject<OtherSettingDto>(otherSettingJStr);
                this._otherSetting = _mapper.Map<OtherSetting>(dto);
                _otherSetting.SaveEvent = SaveChange;
            }

            #endregion 初始化OtherSetting

            #region 初始化PlcAddressCache

            var plcAddressCacheJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _plcAddressCacheSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(plcAddressCacheJStr))
            {
                this._plcValueCacheSetting = new PlcValueCacheSetting();
                //this._plcValueCacheSetting.PlcAddressCache = new List<Model.Entity.PlcCacheValue>();
                this._plcValueCacheSetting.PlcAddressCache = JsonConvert.DeserializeObject<List<Model.Entity.PlcCacheValue>>(plcAddressCacheJStr)
                                                             ?? new List<Model.Entity.PlcCacheValue>();
            }

            #endregion 初始化PlcAddressCache

            var calibrationSettingJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _calibrationSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(calibrationSettingJStr))
            {
                _calibrationSetting = JsonConvert.DeserializeObject<Settings.CalibrationSetting>(calibrationSettingJStr);
                if (_calibrationSetting is { })
                    _calibrationSetting.SaveEvent = SaveChange;
            }

            #region 初始化硬件配置字符串

            PlcSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == _plcSettingName)?.JsonValue;
            OhmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == _ohmSettingName)?.JsonValue;
            DmmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == _dmmSettingName)?.JsonValue;
            SwitchBoardSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == _switchBoardSettingName)?.JsonValue;
            TemperatureSensorJson = context.SettingItems.FirstOrDefault(x => x.SettingName == _temperatureSensorSettingName)?.JsonValue;

            #endregion 初始化硬件配置字符串

            #region 获取数据库连接字符串

            SceneConnectString = context.SettingItems.FirstOrDefault(x => x.SettingName == _sceneConnectStringSettingName)?.JsonValue;

            #endregion 获取数据库连接字符串
        }

        private OcvTypeEnum _ocvType;

        /// <summary>
        /// 当前Ocv类型
        /// </summary>
        public OcvTypeEnum CurrentOcvType
        {
            get { return _ocvType; }
            set
            {
                if (SetProperty(ref _ocvType, value))
                {
                    OnPropertyChanged(nameof(CurrentTestOption));
                    OnPropertyChanged(nameof(CurrentBatteryStandard));
                    OnPropertyChanged(nameof(CurrentFacticity));
                    OnPropertyChanged(nameof(CurrentWmsSetting));
                    OnPropertyChanged(nameof(CurrentTestOrder));
                }
            }
        }

        private readonly FacticitySetting? _facticitySetting;

        /// <summary>
        /// 当前设备真实性信息
        /// </summary>
        public FacticitySetting? CurrentFacticity
        {
            get { return _facticitySetting; }
        }

        private readonly Settings.TestOption? _testOption;

        /// <summary>
        /// 当前TestOption
        /// </summary>
        public Settings.TestOption? CurrentTestOption
        {
            get { return _testOption; }
        }

        private readonly Settings.BatteryStandard? _batteryStandard;

        /// <summary>
        /// 当前BatteryStandard
        /// </summary>
        public Settings.BatteryStandard? CurrentBatteryStandard
        {
            get { return _batteryStandard; }
        }

        private readonly WmsSetting? _wmsSetting;

        /// <summary>
        /// 当前WmsSetting
        /// </summary>
        public WmsSetting? CurrentWmsSetting
        {
            get { return _wmsSetting; }
        }

        private readonly List<List<int>>? _testOrder;

        public List<List<int>>? CurrentTestOrder
        {
            get { return _testOrder; }
        }

        private readonly PlcValueCacheSetting? _plcValueCacheSetting;

        public PlcValueCacheSetting? PlcValueCacheSetting
        {
            get { return _plcValueCacheSetting; }
        }

        private readonly OtherSetting? _otherSetting;

        public OtherSetting? CurrentOtherSetting
        {
            get { return _otherSetting; }
        }

        private readonly Settings.CalibrationSetting? _calibrationSetting;

        public Settings.CalibrationSetting? CurrentCalibrationSetting
        {
            get { return _calibrationSetting; }
        }

        /// <summary>
        /// Plc配置字符串
        /// </summary>
        public string? PlcSettingJson { get; set; }

        /// <summary>
        /// 内阻仪配置字符串
        /// </summary>
        public string? OhmSettingJson { get; set; }

        /// <summary>
        /// 万用表配置字符串
        /// </summary>
        public string? DmmSettingJson { get; set; }

        /// <summary>
        /// 切换板配置字符串
        /// </summary>
        public string? SwitchBoardSettingJson { get; set; }

        /// <summary>
        /// 温度传感器配置字符串
        /// </summary>
        public string? TemperatureSensorJson { get; set; }

        /// <summary>
        /// 现场数据库连接字符串
        /// </summary>
        public string? SceneConnectString { get; set; }

        private OperateResult SaveChange(string name)
        {
            string? settingName = null;
            string? settingValue = null;
            switch (name)
            {
                case "BatteryStandard":
                    settingName = _batteryStandardSettingName;
                    settingValue = JsonConvert.SerializeObject(CurrentBatteryStandard);
                    break;

                case "TestOption":
                    settingName = _testOptionSettingName;
                    settingValue = JsonConvert.SerializeObject(CurrentTestOption);
                    break;

                case "OtherSetting":
                    settingName = _otherSettingName;
                    settingValue = JsonConvert.SerializeObject(CurrentOtherSetting);
                    break;

                case "CalibrationSetting":
                    settingName = _calibrationSettingName;
                    settingValue = JsonConvert.SerializeObject(CurrentCalibrationSetting);
                    break;
            }
            if (!string.IsNullOrEmpty(settingName))
            {
                using var context = new OcvSettingDbContext();
                var item = context.SettingItems.FirstOrDefault(x => x.SettingName == settingName);
                if (item is { })
                {
                    item.JsonValue = settingValue ?? string.Empty;
                    context.SettingItems.Update(item);
                    context.SaveChanges();
                    return OperateResult.CreateSuccessResult();
                }
                else return OperateResult.CreateFailedResult($"未找到{settingName}对应的配置项");
            }
            else return OperateResult.CreateFailedResult($"未找到{name}对应的配置项");
        }
    }
}