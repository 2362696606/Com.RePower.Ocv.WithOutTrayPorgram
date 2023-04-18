using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Byd.CB15.Entities;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Setting;
using Com.RePower.Ocv.Project.Byd.CB15.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers
{
    public partial class SettingManager : ObservableObject
    {
        private readonly string _acirOptionSettingName = "AcirOption";
        private readonly string _facticityManagerSettingName = "FacticityManager";

        [NotifyPropertyChangedFor(nameof(CurrentOtherSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentTestOption))]
        [NotifyPropertyChangedFor(nameof(CurrentBatteryStandard))]
        [NotifyPropertyChangedFor(nameof(CurrentWmeSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentMesSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentPlcAddressCache))]
        [ObservableProperty]
        private OcvTypeEnmu _currentOcvType;

        private readonly OtherSetting? _otherSettingForOcv1;
        private readonly OtherSetting? _otherSettingForOcv2;
        private readonly OtherSetting? _otherSettingForOcv3;
        private readonly OtherSetting? _otherSettingForOcv4;
        private readonly TestOption? _testOptionForOcv1;
        private readonly TestOption? _testOptionForOcv2;
        private readonly TestOption? _testOptionForOcv3;
        private readonly TestOption? _testOptionForOcv4;
        private readonly BatteryStandard? _batteryStandardForOcv1;
        private readonly BatteryStandard? _batteryStandardForOcv2;
        private readonly BatteryStandard? _batteryStandardForOcv3;
        private readonly BatteryStandard? _batteryStandardForOcv4;
        private readonly Dictionary<string, string> _plcAddressCacheForOcv1 = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _plcAddressCacheForOcv2 = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _plcAddressCacheForOcv3 = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _plcAddressCacheForOcv4 = new Dictionary<string, string>();
        private readonly WmsSetting? _wmsSettingForOcv1;
        private readonly WmsSetting? _wmsSettingForOcv2;
        private readonly WmsSetting? _wmsSettingForOcv3;
        private readonly WmsSetting? _wmsSettingForOcv4;
        private readonly MesSetting? _mesSettingForOcv1;
        private readonly MesSetting? _mesSettingForOcv2;
        private readonly MesSetting? _mesSettingForOcv3;
        private readonly MesSetting? _mesSettingForOcv4;
        private List<ChannelNgInfo>? _channelNgInfos;

        private readonly string _channelNgInfosCacheName = "ChannelNgInfoCache";

        private static readonly Lazy<SettingManager> _instance =
           new Lazy<SettingManager>(() => new SettingManager());

        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static SettingManager Instance => _instance.Value;

        private SettingManager()
        {
            using OcvSettingDbContext context = new OcvSettingDbContext();

            #region 初始化OtherSetting

            var otherSettingForOcv1 = context.SettingItems.FirstOrDefault(x => x.SettingName == "OtherSetting_Ocv1");
            var otherSettingForOcv1Str = otherSettingForOcv1?.JsonValue ?? string.Empty;
            this._otherSettingForOcv1 = JsonConvert.DeserializeObject<OtherSetting>(otherSettingForOcv1Str);
            var otherSettingForOcv2 = context.SettingItems.FirstOrDefault(x => x.SettingName == "OtherSetting_Ocv2");
            var otherSettingForOcv2Str = otherSettingForOcv2?.JsonValue ?? string.Empty;
            this._otherSettingForOcv2 = JsonConvert.DeserializeObject<OtherSetting>(otherSettingForOcv2Str);
            var otherSettingForOcv3 = context.SettingItems.FirstOrDefault(x => x.SettingName == "OtherSetting_Ocv3");
            var otherSettingForOcv3Str = otherSettingForOcv3?.JsonValue ?? string.Empty;
            this._otherSettingForOcv3 = JsonConvert.DeserializeObject<OtherSetting>(otherSettingForOcv3Str);
            var otherSettingForOcv4 = context.SettingItems.FirstOrDefault(x => x.SettingName == "OtherSetting_Ocv4");
            var otherSettingForOcv4Str = otherSettingForOcv4?.JsonValue ?? string.Empty;
            this._otherSettingForOcv4 = JsonConvert.DeserializeObject<OtherSetting>(otherSettingForOcv4Str);

            #endregion 初始化OtherSetting

            #region 初始化TestOption

            var testOptionForOcv1Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv1")?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionForOcv1Str))
            {
                this._testOptionForOcv1 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv1Str);
                if (_testOptionForOcv1 is { }) _testOptionForOcv1.SaveEvent = SaveChanged;
            }
            var testOptionForOcv2Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv2")?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionForOcv2Str))
            {
                this._testOptionForOcv2 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv2Str);
                if (_testOptionForOcv2 is { }) _testOptionForOcv2.SaveEvent = SaveChanged;
            }

            var testOptionForOcv3Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv3")?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionForOcv3Str))
            {
                this._testOptionForOcv3 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv3Str);
                if (_testOptionForOcv3 is { }) _testOptionForOcv3.SaveEvent = SaveChanged;
            }
            var testOptionForOcv4Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv4")?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionForOcv4Str))
            {
                this._testOptionForOcv4 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv4Str);
                if (_testOptionForOcv4 is { }) _testOptionForOcv4.SaveEvent = SaveChanged;
            }

            #endregion 初始化TestOption

            #region 初始化BatteryStandard

            var batteryStandardForOcv1Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv1")?.JsonValue;
            if (!string.IsNullOrEmpty(batteryStandardForOcv1Str))
            {
                this._batteryStandardForOcv1 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv1Str);
                if (_batteryStandardForOcv1 is { }) _batteryStandardForOcv1.SaveEvent = SaveChanged;
            }
            var batteryStandardForOcv2Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv2")?.JsonValue;
            if (!string.IsNullOrEmpty(batteryStandardForOcv2Str))
            {
                this._batteryStandardForOcv2 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv2Str);
                if (_batteryStandardForOcv2 is { }) _batteryStandardForOcv2.SaveEvent = SaveChanged;
            }
            var batteryStandardForOcv3Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv3")?.JsonValue;
            if (!string.IsNullOrEmpty(batteryStandardForOcv3Str))
            {
                this._batteryStandardForOcv3 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv3Str);
                if (_batteryStandardForOcv3 is { }) _batteryStandardForOcv3.SaveEvent = SaveChanged;
            }
            var batteryStandardForOcv4Str = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv4")?.JsonValue;
            if (!string.IsNullOrEmpty(batteryStandardForOcv4Str))
            {
                this._batteryStandardForOcv4 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv4Str);
                if (_batteryStandardForOcv4 is { }) _batteryStandardForOcv4.SaveEvent = SaveChanged;
            }

            #endregion 初始化BatteryStandard

            #region 初始化PlcAddressCache

            var plcAddressCacheForOcv1 = context.SettingItems.First(x => x.SettingName == "PlcAddressCache_Ocv1");
            if (plcAddressCacheForOcv1 != null)
            {
                var plcAddressCacheForOcv1Str = plcAddressCacheForOcv1.JsonValue;
                if (!string.IsNullOrEmpty(plcAddressCacheForOcv1Str))
                {
                    JArray localPlcAddressCacheSettingArray = JArray.Parse(plcAddressCacheForOcv1Str);
                    foreach (var item in localPlcAddressCacheSettingArray)
                    {
                        var name = item.Value<string>("Name");
                        var address = item.Value<string>("Address");
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                        {
                            _plcAddressCacheForOcv1.Add(name, address);
                        }
                    }
                }
            }
            var plcAddressCacheForOcv2 = context.SettingItems.First(x => x.SettingName == "PlcAddressCache_Ocv2");
            if (plcAddressCacheForOcv2 != null)
            {
                var plcAddressCacheForOcv2Str = plcAddressCacheForOcv2.JsonValue;
                if (!string.IsNullOrEmpty(plcAddressCacheForOcv2Str))
                {
                    JArray localPlcAddressCacheSettingArray = JArray.Parse(plcAddressCacheForOcv2Str);
                    foreach (var item in localPlcAddressCacheSettingArray)
                    {
                        var name = item.Value<string>("Name");
                        var address = item.Value<string>("Address");
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                        {
                            _plcAddressCacheForOcv2.Add(name, address);
                        }
                    }
                }
            }
            var plcAddressCacheForOcv3 = context.SettingItems.First(x => x.SettingName == "PlcAddressCache_Ocv3");
            if (plcAddressCacheForOcv3 != null)
            {
                var plcAddressCacheForOcv3Str = plcAddressCacheForOcv3.JsonValue;
                if (!string.IsNullOrEmpty(plcAddressCacheForOcv3Str))
                {
                    JArray localPlcAddressCacheSettingArray = JArray.Parse(plcAddressCacheForOcv3Str);
                    foreach (var item in localPlcAddressCacheSettingArray)
                    {
                        var name = item.Value<string>("Name");
                        var address = item.Value<string>("Address");
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                        {
                            _plcAddressCacheForOcv3.Add(name, address);
                        }
                    }
                }
            }
            var plcAddressCacheForOcv4 = context.SettingItems.First(x => x.SettingName == "PlcAddressCache_Ocv4");
            if (plcAddressCacheForOcv4 != null)
            {
                var plcAddressCacheForOcv4Str = plcAddressCacheForOcv4.JsonValue;
                if (!string.IsNullOrEmpty(plcAddressCacheForOcv4Str))
                {
                    JArray localPlcAddressCacheSettingArray = JArray.Parse(plcAddressCacheForOcv4Str);
                    foreach (var item in localPlcAddressCacheSettingArray)
                    {
                        var name = item.Value<string>("Name");
                        var address = item.Value<string>("Address");
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                        {
                            _plcAddressCacheForOcv4.Add(name, address);
                        }
                    }
                }
            }

            #endregion 初始化PlcAddressCache

            #region 初始化WmsSetting

            var wmsSettingForOcv1 = context.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv1");
            var wmsSettingForOcv1Str = wmsSettingForOcv1?.JsonValue ?? string.Empty;
            this._wmsSettingForOcv1 = JsonConvert.DeserializeObject<WmsSetting>(wmsSettingForOcv1Str);
            var wmsSettingForOcv2 = context.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv2");
            var wmsSettingForOcv2Str = wmsSettingForOcv2?.JsonValue ?? string.Empty;
            this._wmsSettingForOcv2 = JsonConvert.DeserializeObject<WmsSetting>(wmsSettingForOcv2Str);
            var wmsSettingForOcv3 = context.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv3");
            var wmsSettingForOcv3Str = wmsSettingForOcv3?.JsonValue ?? string.Empty;
            this._wmsSettingForOcv3 = JsonConvert.DeserializeObject<WmsSetting>(wmsSettingForOcv3Str);
            var wmsSettingForOcv4 = context.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv4");
            var wmsSettingForOcv4Str = wmsSettingForOcv4?.JsonValue ?? string.Empty;
            this._wmsSettingForOcv4 = JsonConvert.DeserializeObject<WmsSetting>(wmsSettingForOcv4Str);

            #endregion 初始化WmsSetting

            #region 初始化MesSetting

            var mesSettingForOcv1 = context.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv1");
            var mesSettingForOcv1Str = mesSettingForOcv1?.JsonValue ?? string.Empty;
            this._mesSettingForOcv1 = JsonConvert.DeserializeObject<MesSetting>(mesSettingForOcv1Str);
            var mesSettingForOcv2 = context.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv2");
            var mesSettingForOcv2Str = mesSettingForOcv2?.JsonValue ?? string.Empty;
            this._mesSettingForOcv2 = JsonConvert.DeserializeObject<MesSetting>(mesSettingForOcv2Str);
            var mesSettingForOcv3 = context.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv3");
            var mesSettingForOcv3Str = mesSettingForOcv3?.JsonValue ?? string.Empty;
            this._mesSettingForOcv3 = JsonConvert.DeserializeObject<MesSetting>(mesSettingForOcv3Str);
            var mesSettingForOcv4 = context.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv4");
            var mesSettingForOcv4Str = mesSettingForOcv4?.JsonValue ?? string.Empty;
            this._mesSettingForOcv4 = JsonConvert.DeserializeObject<MesSetting>(mesSettingForOcv4Str);

            #endregion 初始化MesSetting

            #region 初始化默认工站

            var defaultOcvType = context.SettingItems.FirstOrDefault(x => x.SettingName == "DefaultOcvType");
            string defaultOcvTypeStr = defaultOcvType?.JsonValue ?? string.Empty;
            this.CurrentOcvType = Enum.Parse<OcvTypeEnmu>(defaultOcvTypeStr);

            #endregion 初始化默认工站

            #region 初始化ChannelNgInfos

            var channelNgInfoCacheStr = context.CacheValues.FirstOrDefault(x => x.SettingName == _channelNgInfosCacheName)?.Value;
            if (!string.IsNullOrEmpty(channelNgInfoCacheStr))
            {
                _channelNgInfos = JsonConvert.DeserializeObject<List<ChannelNgInfo>>(channelNgInfoCacheStr);
            }
            if (_channelNgInfos == null || _channelNgInfos.Count() < (CurrentTestOption?.BatteryCount ?? 0))
            {
                _channelNgInfos = new List<ChannelNgInfo>();
                for (int i = 1; i <= (CurrentTestOption?.BatteryCount ?? 0); i++)
                {
                    ChannelNgInfo channelNgInfo = new ChannelNgInfo { Channel = i, NgTimes = 0 };
                    _channelNgInfos.Add(channelNgInfo);
                }
            }
            foreach (var item in _channelNgInfos)
            {
                item.SaveEvent = SaveChannelNgInfo;
            }

            #endregion 初始化ChannelNgInfos

            var facticityManagerStr = context.SettingItems.FirstOrDefault(x => x.SettingName == "FacticityManager")?.JsonValue;
            if (!string.IsNullOrEmpty(facticityManagerStr))
            {
                FacticityManager = JsonConvert.DeserializeObject<FacticityManager>(facticityManagerStr) ?? new FacticityManager();
            }
            else
            {
                FacticityManager = new FacticityManager();
            }

            if (CurrentOcvType == OcvTypeEnmu.OCV4)
            {
                var acirOptionStr = context.SettingItems.FirstOrDefault(x => x.SettingName == _acirOptionSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(acirOptionStr))
                {
                    _acirOption = JsonConvert.DeserializeObject<AcirOption>(acirOptionStr);
                }
                else
                {
                    _acirOption = new AcirOption();
                }
                if (_acirOption is { })
                    _acirOption.SaveEvent = SaveChanged;
            }
        }

        /// <summary>
        /// 其他设置
        /// </summary>
        public OtherSetting? CurrentOtherSetting
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _otherSettingForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _otherSettingForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _otherSettingForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _otherSettingForOcv4;

                    default: return null;
                }
            }
        }

        public TestOption? CurrentTestOption
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _testOptionForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _testOptionForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _testOptionForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _testOptionForOcv4;

                    default: return null;
                }
            }
        }

        public BatteryStandard? CurrentBatteryStandard
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _batteryStandardForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _batteryStandardForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _batteryStandardForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _batteryStandardForOcv4;

                    default: return null;
                }
            }
        }

        public Dictionary<string, string> CurrentPlcAddressCache
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _plcAddressCacheForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _plcAddressCacheForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _plcAddressCacheForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _plcAddressCacheForOcv4;

                    default: return new Dictionary<string, string>();
                }
            }
        }

        public WmsSetting? CurrentWmeSetting
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _wmsSettingForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _wmsSettingForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _wmsSettingForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _wmsSettingForOcv4;

                    default: return null;
                }
            }
        }

        public MesSetting? CurrentMesSetting
        {
            get
            {
                switch (CurrentOcvType)
                {
                    case OcvTypeEnmu.OCV1:
                        return _mesSettingForOcv1;

                    case OcvTypeEnmu.OCV2:
                        return _mesSettingForOcv2;

                    case OcvTypeEnmu.OCV3:
                        return _mesSettingForOcv3;

                    case OcvTypeEnmu.OCV4:
                        return _mesSettingForOcv4;

                    default: return null;
                }
            }
        }

        public FacticityManager FacticityManager { get; set; }

        private readonly AcirOption? _acirOption;

        public AcirOption? AcirOption
        {
            get { return _acirOption; }
        }

        public List<ChannelNgInfo>? ChannelNgInfos
        {
            get { return _channelNgInfos; }
            set { SetProperty(ref _channelNgInfos, value); }
        }

        private OperateResult SaveChanged(string name)
        {
            string? settingName = null;
            string? settingValue = null;
            switch (name)
            {
                case "TestOption":
                    {
                        switch (CurrentOcvType)
                        {
                            case OcvTypeEnmu.OCV1:
                                settingName = "TestOption_Ocv1";
                                break;

                            case OcvTypeEnmu.OCV2:
                                settingName = "TestOption_Ocv2";
                                break;

                            case OcvTypeEnmu.OCV3:
                                settingName = "TestOption_Ocv3";
                                break;

                            case OcvTypeEnmu.OCV4:
                                settingName = "TestOption_Ocv4";
                                break;
                        }
                        settingValue = JsonConvert.SerializeObject(CurrentTestOption);
                        break;
                    }
                case "BatteryStandard":
                    {
                        switch (CurrentOcvType)
                        {
                            case OcvTypeEnmu.OCV1:
                                settingName = "BatteryStandard_Ocv1";
                                break;

                            case OcvTypeEnmu.OCV2:
                                settingName = "BatteryStandard_Ocv2";
                                break;

                            case OcvTypeEnmu.OCV3:
                                settingName = "BatteryStandard_Ocv3";
                                break;

                            case OcvTypeEnmu.OCV4:
                                settingName = "BatteryStandard_Ocv4";
                                break;
                        }
                        settingValue = JsonConvert.SerializeObject(CurrentBatteryStandard);
                        break;
                    }
                case "AcirOption":
                    {
                        settingName = _acirOptionSettingName;
                        settingValue = JsonConvert.SerializeObject(AcirOption);
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(settingName))
            {
                try
                {
                    using (var context = new OcvSettingDbContext())
                    {
                        var item = context.SettingItems.FirstOrDefault(x => x.SettingName == settingName);
                        if (item is { })
                        {
                            item.JsonValue = settingValue ?? string.Empty;
                            context.SettingItems.Update(item);
                            context.SaveChanges();
                        }
                        else
                            return OperateResult.CreateFailedResult($"未找到{settingName}对应的配置项");
                    }
                    return OperateResult.CreateSuccessResult();
                }
                catch (Exception e)
                {
                    return OperateResult.CreateFailedResult(e.Message);
                    throw;
                }
            }
            else
            {
                return OperateResult.CreateFailedResult($"未找到{name}对应配置项");
            }
        }

        private OperateResult SaveChannelNgInfo()
        {
            var jStr = JsonConvert.SerializeObject(ChannelNgInfos);
            using (var context = new OcvSettingDbContext())
            {
                try
                {
                    var item = context.SettingItems.FirstOrDefault(x => x.SettingName == _channelNgInfosCacheName);
                    if (item is { })
                    {
                        item.JsonValue = jStr ?? string.Empty;
                        context.SettingItems.Update(item);
                        context.SaveChanges();
                    }
                    else
                        return OperateResult.CreateFailedResult($"未找到{_channelNgInfosCacheName}对应的缓存项");
                }
                catch (Exception e)
                {
                    return OperateResult.CreateFailedResult($"保存通道Ng信息失败:{e.Message},内部异常：{e.InnerException}");
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}