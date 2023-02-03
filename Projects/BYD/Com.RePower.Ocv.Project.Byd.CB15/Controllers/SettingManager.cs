using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Setting;
using Com.RePower.Ocv.Project.Byd.CB15.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers
{
    public partial class SettingManager: ObservableObject
    {
        [NotifyPropertyChangedFor(nameof(CurrentOtherSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentTestOption))]
        [NotifyPropertyChangedFor(nameof(CurrentBatteryStandard))]
        [ObservableProperty]
        private OcvTypeEnmu _currentOcvType;

        private OtherSetting? _otherSettingForOcv1;
        private OtherSetting? _otherSettingForOcv2;
        private OtherSetting? _otherSettingForOcv3;
        private OtherSetting? _otherSettingForOcv4;
        private TestOption? _testOptionForOcv1;
        private TestOption? _testOptionForOcv2;
        private TestOption? _testOptionForOcv3;
        private TestOption? _testOptionForOcv4;
        private BatteryStandard? _batteryStandardForOcv1;
        private BatteryStandard? _batteryStandardForOcv2;
        private BatteryStandard? _batteryStandardForOcv3;
        private BatteryStandard? _batteryStandardForOcv4;
        private Dictionary<string,string> _plcAddressCacheForOcv1 = new Dictionary<string,string>();
        private Dictionary<string,string> _plcAddressCacheForOcv2 = new Dictionary<string,string>();
        private Dictionary<string,string> _plcAddressCacheForOcv3 = new Dictionary<string,string>();
        private Dictionary<string,string> _plcAddressCacheForOcv4 = new Dictionary<string,string>();
        private WmsSetting? _wmsSettingForOcv1;
        private WmsSetting? _wmsSettingForOcv2;
        private WmsSetting? _wmsSettingForOcv3;
        private WmsSetting? _wmsSettingForOcv4;

        public SettingManager()
        {
            using (OcvSettingDbContext context = new OcvSettingDbContext())
            {
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
                #endregion
                #region 初始化TestOption
                var testOptionForOcv1 = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv1");
                var testOptionForOcv1Str = testOptionForOcv1?.JsonValue ?? string.Empty;
                this._testOptionForOcv1 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv1Str);
                var testOptionForOcv2 = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv2");
                var testOptionForOcv2Str = testOptionForOcv2?.JsonValue ?? string.Empty;
                this._testOptionForOcv2 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv2Str);
                var testOptionForOcv3 = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv3");
                var testOptionForOcv3Str = testOptionForOcv3?.JsonValue ?? string.Empty;
                this._testOptionForOcv3 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv3Str);
                var testOptionForOcv4 = context.SettingItems.FirstOrDefault(x => x.SettingName == "TestOption_Ocv4");
                var testOptionForOcv4Str = testOptionForOcv4?.JsonValue ?? string.Empty;
                this._testOptionForOcv4 = JsonConvert.DeserializeObject<TestOption>(testOptionForOcv4Str);
                #endregion
                #region 初始化BatteryStandard
                var BatteryStandardForOcv1 = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv1");
                var BatteryStandardForOcv1Str = BatteryStandardForOcv1?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv1 = JsonConvert.DeserializeObject<BatteryStandard>(BatteryStandardForOcv1Str);
                var BatteryStandardForOcv2 = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv2");
                var BatteryStandardForOcv2Str = BatteryStandardForOcv2?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv2 = JsonConvert.DeserializeObject<BatteryStandard>(BatteryStandardForOcv2Str);
                var BatteryStandardForOcv3 = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv3");
                var BatteryStandardForOcv3Str = BatteryStandardForOcv3?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv3 = JsonConvert.DeserializeObject<BatteryStandard>(BatteryStandardForOcv3Str);
                var BatteryStandardForOcv4 = context.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandard_Ocv4");
                var BatteryStandardForOcv4Str = BatteryStandardForOcv4?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv4 = JsonConvert.DeserializeObject<BatteryStandard>(BatteryStandardForOcv4Str);
                #endregion
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
                #endregion

                #region 初始化TestOption
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
                #endregion

                #region 初始化默认工站
                var defaultOcvType = context.SettingItems.FirstOrDefault(x => x.SettingName == "DefaultOcvType");
                string defaultOcvTypeStr = defaultOcvType?.JsonValue ?? string.Empty;
                this.CurrentOcvType = Enum.Parse<OcvTypeEnmu>(defaultOcvTypeStr); 
                #endregion
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
                    default: return new Dictionary<string,string>();
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
    }
}
