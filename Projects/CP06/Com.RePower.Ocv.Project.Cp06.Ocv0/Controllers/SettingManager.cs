using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BatteryStandard = Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings.BatteryStandard;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public partial class SettingManager:ObservableObject
    {
        private readonly string calibrationSettingName = "CalibrationSetting";

        private static readonly Lazy<SettingManager> _instance =
           new Lazy<SettingManager>(() => new SettingManager());
        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static SettingManager Instance { get { return _instance.Value; } }

        private WmsSetting? _wmsSettingForOcv0;
        private WmsSetting? _wmsSettingForOcv1;
        private WmsSetting? _wmsSettingForOcv2;
        private WmsSetting? _wmsSettingForOcv3;
        private MesSetting? _mesSettingForOcv0;
        private MesSetting? _mesSettingForOcv1;
        private MesSetting? _mesSettingForOcv2;
        private MesSetting? _mesSettingForOcv3;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentWmsSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentMesSetting))]
        [NotifyPropertyChangedFor(nameof(CurrentTestOption))]
        [NotifyPropertyChangedFor(nameof(CurrentBatteryStandard))]
        private OcvTypeEnmu _currentOcvType;
        //private TestOption _testOption;
        private Services.Settings.BatteryStandard? _batteryStandardForOcv0;
        private Services.Settings.BatteryStandard? _batteryStandardForOcv1;
        private Services.Settings.BatteryStandard? _batteryStandardForOcv2;
        private Services.Settings.BatteryStandard? _batteryStandardForOcv3;
        private Services.Settings.TestOption? _testOptionForOcv0;
        private Services.Settings.TestOption? _testOptionForOcv1;
        private Services.Settings.TestOption? _testOptionForOcv2;
        private Services.Settings.TestOption? _testOptionForOcv3;
        private Services.Settings.FacticitySetting? _facticitySetting;
        private List<List<int>> _testOrder;

        private SettingManager()
        {
            using (var settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "DefaultOcvType");
                string defaultOcvTypeStr = defaultOcvType?.JsonValue ?? string.Empty;
                #region 初始化wms配置
                var wmsOcv0 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv0");
                string jStrOcv0 = wmsOcv0?.JsonValue??string.Empty;
                var wmsOcv1 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv1");
                string jStrOcv1 = wmsOcv1?.JsonValue ?? string.Empty;
                var wmsOcv2 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv2");
                string jStrOcv2 = wmsOcv2?.JsonValue ?? string.Empty;
                var wmsOcv3 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "WmsSetting_Ocv3");
                string jStrOcv3 = wmsOcv3?.JsonValue ?? string.Empty;
                this._wmsSettingForOcv0 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv0);
                this._wmsSettingForOcv1 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv1);
                this._wmsSettingForOcv2 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv2);
                this._wmsSettingForOcv3 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv3);
                #endregion
                #region 初始化mes配置
                var mesOcv0 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv0");
                string jStrMesOcv0 = mesOcv0?.JsonValue ?? string.Empty;
                var mesOcv1 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv1");
                string jStrMesOcv1 = mesOcv1?.JsonValue ?? string.Empty;
                var mesOcv2 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv2");
                string jStrMesOcv2 = mesOcv2?.JsonValue ?? string.Empty;
                var mesOcv3 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "MesSetting_Ocv3");
                string jStrMesOcv3 = mesOcv3?.JsonValue ?? string.Empty;
                this._mesSettingForOcv0 = JsonConvert.DeserializeObject<MesSetting>(jStrMesOcv0);
                this._mesSettingForOcv1 = JsonConvert.DeserializeObject<MesSetting>(jStrMesOcv1);
                this._mesSettingForOcv2 = JsonConvert.DeserializeObject<MesSetting>(jStrMesOcv2);
                this._mesSettingForOcv3 = JsonConvert.DeserializeObject<MesSetting>(jStrMesOcv3);
                #endregion
                #region 初始化TestOption
                var testOptionForOcv0 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "TestOptionForOcv0");
                var testOptionForOcv0Str = testOptionForOcv0?.JsonValue ?? string.Empty;
                this._testOptionForOcv0 = JsonConvert.DeserializeObject<Services.Settings.TestOption>(testOptionForOcv0Str);
                if (_testOptionForOcv0 is ISettingSaveChanged)
                    _testOptionForOcv0.DoSaveChanged = DoSaveChanged;
                var testOptionForOcv1 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "TestOptionForOcv1");
                var testOptionForOcv1Str = testOptionForOcv1?.JsonValue ?? string.Empty;
                this._testOptionForOcv1 = JsonConvert.DeserializeObject<Services.Settings.TestOption>(testOptionForOcv1Str);
                if (_testOptionForOcv1 is ISettingSaveChanged)
                    _testOptionForOcv1.DoSaveChanged = DoSaveChanged;
                var testOptionForOcv2 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "TestOptionForOcv2");
                var testOptionForOcv2Str = testOptionForOcv2?.JsonValue ?? string.Empty;
                this._testOptionForOcv2 = JsonConvert.DeserializeObject<Services.Settings.TestOption>(testOptionForOcv2Str);
                if (_testOptionForOcv2 is ISettingSaveChanged)
                    _testOptionForOcv2.DoSaveChanged = DoSaveChanged;
                var testOptionForOcv3 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "TestOptionForOcv3");
                var testOptionForOcv3Str = testOptionForOcv3?.JsonValue ?? string.Empty;
                this._testOptionForOcv3 = JsonConvert.DeserializeObject<Services.Settings.TestOption>(testOptionForOcv3Str);
                if (_testOptionForOcv3 is ISettingSaveChanged)
                    _testOptionForOcv3.DoSaveChanged = DoSaveChanged;
                #endregion
                #region 初始化BatteryStatdard
                var batteryStandardForOcv0 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandardForOcv0");
                var batteryStandardForOcv0Str = batteryStandardForOcv0?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv0 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv0Str);
                if (_batteryStandardForOcv0 is ISettingSaveChanged)
                    _batteryStandardForOcv0.DoSaveChanged = DoSaveChanged;
                var batteryStandardForOcv1 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandardForOcv1");
                var batteryStandardForOcv1Str = batteryStandardForOcv1?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv1 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv1Str);
                if (_batteryStandardForOcv1 is ISettingSaveChanged)
                    _batteryStandardForOcv1.DoSaveChanged = DoSaveChanged;
                var batteryStandardForOcv2 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandardForOcv2");
                var batteryStandardForOcv2Str = batteryStandardForOcv2?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv2 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv2Str);
                if (_batteryStandardForOcv2 is ISettingSaveChanged)
                    _batteryStandardForOcv2.DoSaveChanged = DoSaveChanged;
                var batteryStandardForOcv3 = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "BatteryStandardForOcv3");
                var batteryStandardForOcv3Str = batteryStandardForOcv3?.JsonValue ?? string.Empty;
                this._batteryStandardForOcv3 = JsonConvert.DeserializeObject<BatteryStandard>(batteryStandardForOcv3Str);
                if (_batteryStandardForOcv3 is ISettingSaveChanged)
                    _batteryStandardForOcv3.DoSaveChanged = DoSaveChanged;
                #endregion
                #region 初始化TestOrder
                var item = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "TestOrder");
                string jStr = item?.JsonValue ?? string.Empty;
                this._testOrder = JsonConvert.DeserializeObject<List<List<int>>>(jStr) ?? new List<List<int>>();
                #endregion
                #region 初始化FacticitySetting
                var facticitySettingStr = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "FacticityManager")?.JsonValue;
                if (!string.IsNullOrEmpty(facticitySettingStr))
                {
                    this._facticitySetting = JsonConvert.DeserializeObject<Services.Settings.FacticitySetting>(facticitySettingStr);
                }
                #endregion
                #region 初始化CalibrationSetting
                var calibrationSettingStr = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == calibrationSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(calibrationSettingStr))
                    this._calibrationSetting = JsonConvert.DeserializeObject<Services.Settings.CalibrationSetting>(calibrationSettingStr);
                if (_calibrationSetting is ISettingSaveChanged)
                    _calibrationSetting.DoSaveChanged = DoSaveChanged;
                #endregion

                this.CurrentOcvType = Enum.Parse<OcvTypeEnmu>(defaultOcvTypeStr);
            }
        }
        public WmsSetting? CurrentWmsSetting
        {
            get 
            {
                switch(_currentOcvType)
                {
                    case OcvTypeEnmu.OCV0:
                        return _wmsSettingForOcv0;
                    case OcvTypeEnmu.OCV1:
                        return _wmsSettingForOcv1;
                    case OcvTypeEnmu.OCV2:
                        return _wmsSettingForOcv2;
                    case OcvTypeEnmu.OCV3:
                        return _wmsSettingForOcv3;
                    default:
                        return null;
                }
            }
        }
        public MesSetting? CurrentMesSetting
        {
            get
            {
                switch (_currentOcvType)
                {
                    case OcvTypeEnmu.OCV0:
                        return _mesSettingForOcv0;
                    case OcvTypeEnmu.OCV1:
                        return _mesSettingForOcv1;
                    case OcvTypeEnmu.OCV2:
                        return _mesSettingForOcv2;
                    case OcvTypeEnmu.OCV3:
                        return _mesSettingForOcv3;
                    default:
                        return null;
                }
            }
        }
        public Services.Settings.TestOption? CurrentTestOption 
        {
            get
            {
                switch (_currentOcvType)
                {
                    case OcvTypeEnmu.OCV0:
                        return _testOptionForOcv0;
                    case OcvTypeEnmu.OCV1:
                        return _testOptionForOcv1;
                    case OcvTypeEnmu.OCV2:
                        return _testOptionForOcv2;
                    case OcvTypeEnmu.OCV3:
                        return _testOptionForOcv3;
                    default:
                        return null;
                }
            }
        }
        public Services.Settings.BatteryStandard? CurrentBatteryStandard 
        {
            get
            {
                switch (_currentOcvType)
                {
                    case OcvTypeEnmu.OCV0:
                        return _batteryStandardForOcv0;
                    case OcvTypeEnmu.OCV1:
                        return _batteryStandardForOcv1;
                    case OcvTypeEnmu.OCV2:
                        return _batteryStandardForOcv2;
                    case OcvTypeEnmu.OCV3:
                        return _batteryStandardForOcv3;
                    default:
                        return null;
                }
            }
        }
        private Services.Settings.CalibrationSetting? _calibrationSetting;

        public Services.Settings.CalibrationSetting? CurrentCalibrationSetting
        {
            get { return _calibrationSetting; }
        }


        /// <summary>
        /// 设备真实性配置
        /// </summary>
        public Services.Settings.FacticitySetting? FacticitySetting
        {
            get { return _facticitySetting; }
        }

        /// <summary>
        /// 测试顺序
        /// </summary>
        public List<List<int>> CurrentTestOrder
        {
            get { return _testOrder; }
        }

        private OperateResult DoSaveChanged(string name)
        {
            string? settingName = null;
            string? jStr = null;
            switch(name)
            {
                case "TestOption":
                    {
                        switch (CurrentOcvType)
                        {
                            case OcvTypeEnmu.OCV0:
                                settingName = "TestOptionForOcv0";
                                break;
                            case OcvTypeEnmu.OCV1:
                                settingName = "TestOptionForOcv1";
                                break;
                            case OcvTypeEnmu.OCV2:
                                settingName = "TestOptionForOcv2";
                                break;
                            case OcvTypeEnmu.OCV3:
                                settingName = "TestOptionForOcv3";
                                break;
                        }
                        jStr = JsonConvert.SerializeObject(CurrentTestOption);
                        break;
                    }
                case "BatteryStandard":
                    {
                        switch (CurrentOcvType)
                        {
                            case OcvTypeEnmu.OCV0:
                                settingName = "BatteryStandardForOcv0";
                                break;
                            case OcvTypeEnmu.OCV1:
                                settingName = "BatteryStandardForOcv1";
                                break;
                            case OcvTypeEnmu.OCV2:
                                settingName = "BatteryStandardForOcv2";
                                break;
                            case OcvTypeEnmu.OCV3:
                                settingName = "BatteryStandardForOcv3";
                                break;
                        }
                        jStr = JsonConvert.SerializeObject(CurrentBatteryStandard);
                        break;
                    }
                case "Calibration":
                    {
                        jStr = JsonConvert.SerializeObject(CurrentCalibrationSetting);
                        if (!string.IsNullOrEmpty(jStr))
                            settingName = calibrationSettingName;
                        break;
                    }
                default:
                    return OperateResult.CreateFailedResult($"未实现{name}的保存修改");
            }
            if(string.IsNullOrEmpty(settingName))
            {
                return OperateResult.CreateFailedResult($"未实现\"{name}\"的保存修改");
            }
            else
            {
                using (var settingContext = new OcvSettingDbContext())
                {
                    var item = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == settingName);
                    if (item is { })
                    {
                        item.JsonValue = jStr;
                        settingContext.Update(item);
                        settingContext.SaveChanges();
                    }
                    else
                        return OperateResult.CreateFailedResult($"无法在配置数据库中找到\"{settingName}\"对应的配置项");
                }
                return OperateResult.CreateSuccessResult(); 
            }
        }
    }
}
