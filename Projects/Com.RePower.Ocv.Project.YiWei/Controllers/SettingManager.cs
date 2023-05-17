using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Project.YiWei.Model;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Linq;
using Com.RePower.Ocv.Model.Dto;
using CommunityToolkit.Mvvm.ComponentModel;
using TestOption = Com.RePower.Ocv.Project.YiWei.Model.TestOption;

namespace Com.RePower.Ocv.Project.YiWei.Controllers
{
    public class SettingManager:ObservableObject
    {
        private readonly string _testOptionSettingName1 = "测试选项1";
        private readonly string _testOptionSettingName2 = "测试选项2";
        private readonly string _testOptionSettingName3 = "测试选项3";
        private readonly string _testOptionSettingName4 = "测试选项4";
        private readonly string _testOptionSettingName5 = "测试选项5";
        private readonly string _batteryNgCriteriaSettingName1 = "电池标准1";
        private readonly string _batteryNgCriteriaSettingName2 = "电池标准2";
        private readonly string _batteryNgCriteriaSettingName3 = "电池标准3";
        private readonly string _batteryNgCriteriaSettingName4 = "电池标准4";
        private readonly string _batteryNgCriteriaSettingName5 = "电池标准5";
        private readonly string _facticitySettingName = "FacticitySetting";
        private readonly string _plcSettingName = "本地Plc";
        private readonly string _ohmSettingName = "内阻仪";
        private readonly string _dmmSettingName = "万用表";
        private readonly string _switchBoardSettingName = "切换板";
        private readonly string _plcCacheValueSettingName = "本地Plc缓存";
        private readonly string _plcAlarmCacheValueSettingName = "Plc报警缓存";
        private readonly string _calibrationSettingName = "校准选项";
        private readonly string _configSelected = "配置选择";


        private SettingManager()
        {
            using (var dbContext = new OcvSettingDbContext())
            {
                #region 初始化TestOption
                //var testOptionSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _testOptionSettingName)?.JsonValue;
                //if (!string.IsNullOrEmpty(testOptionSettingJson))
                //{
                //    _testOption = JsonConvert.DeserializeObject<Model.TestOption>(testOptionSettingJson);
                //    if (_testOption is { }) _testOption.SaveAction = SaveSettingChanged;
                //}
                for (int i = 1; i < 6; i++)
                {
                    InitTestOption(dbContext, i);
                }
                #endregion
                #region 初始化配置选择

                var configSelectJStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _configSelected)?.JsonValue;
                if (string.IsNullOrEmpty(configSelectJStr))
                {
                    _configSelectedEnum = ConfigSelectedEnum.Config1;
                    dbContext.SettingItems.Add(new OcvSettingItemDto()
                        { SettingName = _configSelected, JsonValue = _configSelectedEnum.ToString() });
                }
                else
                {
                    _configSelectedEnum = System.Enum.Parse<ConfigSelectedEnum>(configSelectJStr);
                }
                #endregion

                #region 初始化BatteryNgCriteria
                //var ngCriteriaJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _batteryNgCriteria1SettingName)?.JsonValue;
                //if (!string.IsNullOrEmpty(ngCriteriaJsonStr))
                //{
                //    _batteryNgCriteria1 = JsonConvert.DeserializeObject<BatteryNgCriteria>(ngCriteriaJsonStr);
                //    if(_batteryNgCriteria1 is { }) _batteryNgCriteria1.SaveAction = SaveSettingChanged;
                //}
                for (int i = 1; i < 6; i++)
                {
                    InitBatteryCriteria1(dbContext, i);
                }
                
                #endregion

                var calibrationJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _calibrationSettingName)?.JsonValue;
                if(!string.IsNullOrEmpty(calibrationJsonStr))
                {
                    _calibrationSetting = JsonConvert.DeserializeObject<Model.CalibrationSetting>(calibrationJsonStr);
                    if(_calibrationSetting is { }) _calibrationSetting.SaveAction = SaveSettingChanged;
                }

                #region 设备真实性配置
                var facticitySettingJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _facticitySettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(facticitySettingJsonStr))
                    _facticitySetting = JsonConvert.DeserializeObject<FacticitySetting>(facticitySettingJsonStr); 
                #endregion

                #region 获取硬件配置字符串
                PlcSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _plcSettingName)?.JsonValue;
                OhmSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _ohmSettingName)?.JsonValue;
                DmmSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _dmmSettingName)?.JsonValue;
                SwitchBoardSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _switchBoardSettingName)?.JsonValue;


                PlcCacheJsonValue = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _plcCacheValueSettingName)?.JsonValue;
                PlcAlarmCacheJsonValue = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _plcAlarmCacheValueSettingName)?.JsonValue;
                #endregion

            }
        }

        private void InitBatteryCriteria1(OcvSettingDbContext dbContext, int i)
        {
            string settingName;
            switch (i)
            {
                case 1:
                    settingName = _batteryNgCriteriaSettingName1;
                    break;
                case 2:
                    settingName = _batteryNgCriteriaSettingName2;
                    break;
                case 3:
                    settingName = _batteryNgCriteriaSettingName3;
                    break;
                case 4:
                    settingName = _batteryNgCriteriaSettingName4;
                    break;
                case 5:
                    settingName = _batteryNgCriteriaSettingName5;
                    break;
                default:
                    settingName = _batteryNgCriteriaSettingName1;
                    break;
            }
            BatteryNgCriteria ngCriterial = new BatteryNgCriteria();

            var criteriaStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == settingName)?.JsonValue;
            if (string.IsNullOrEmpty(criteriaStr))
            {
                ngCriterial = new BatteryNgCriteria();
                string tempJStr = JsonConvert.SerializeObject(ngCriterial);
                dbContext.SettingItems.Add(new OcvSettingItemDto()
                    { SettingName = settingName, JsonValue = tempJStr });
                dbContext.SaveChanges();
            }
            else
            {
                ngCriterial = JsonConvert.DeserializeObject<BatteryNgCriteria>(criteriaStr) ??
                              throw new Exception("初始化电池范围1失败");
            }

            ngCriterial.SaveAction = SaveSettingChanged;
            switch (i)
            {
                case 1:
                    _batteryNgCriteria1 = ngCriterial;
                    break;
                case 2:
                    _batteryNgCriteria2 = ngCriterial;
                    break;
                case 3:
                    _batteryNgCriteria3 = ngCriterial;
                    break;
                case 4:
                    _batteryNgCriteria4 = ngCriterial;
                    break;
                case 5:
                    _batteryNgCriteria5 = ngCriterial;
                    break;
                default:
                    _batteryNgCriteria1 = ngCriterial; 
                    break;
            }
        }


        private void InitTestOption(OcvSettingDbContext dbContext, int i)
        {
            string settingName;
            switch (i)
            {
                case 1:
                    settingName = _testOptionSettingName1;
                    break;
                case 2:
                    settingName = _testOptionSettingName2;
                    break;
                case 3:
                    settingName = _testOptionSettingName3;
                    break;
                case 4:
                    settingName = _testOptionSettingName4;
                    break;
                case 5:
                    settingName = _testOptionSettingName5;
                    break;
                default:
                    settingName = _testOptionSettingName1;
                    break;
            }
            TestOption testOption = new TestOption();

            var testOptionStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == settingName)?.JsonValue;
            if (string.IsNullOrEmpty(testOptionStr))
            {
                testOption = new TestOption();
                string tempJStr = JsonConvert.SerializeObject(testOption);
                dbContext.SettingItems.Add(new OcvSettingItemDto()
                { SettingName = settingName, JsonValue = tempJStr });
                dbContext.SaveChanges();
            }
            else
            {
                testOption = JsonConvert.DeserializeObject<TestOption>(testOptionStr) ??
                              throw new Exception($"初始化测试选项{i}失败");
            }

            testOption.SaveAction = SaveSettingChanged;
            switch (i)
            {
                case 1:
                    _testOption1 = testOption;
                    break;
                case 2:
                    _testOption2 = testOption;
                    break;
                case 3:
                    _testOption3 = testOption;
                    break;
                case 4:
                    _testOption4 = testOption;
                    break;
                case 5:
                    _testOption5 = testOption;
                    break;
                default:
                    _testOption1 = testOption;
                    break;
            }
        }

        private Model.CalibrationSetting? _calibrationSetting;

        public Model.CalibrationSetting? CurrentCalibrationSetting
        {
            get { return _calibrationSetting; }
            set { _calibrationSetting = value; }
        }

        private ConfigSelectedEnum _configSelectedEnum;
        public ConfigSelectedEnum CurrentConfigSelectedEnum
        {
            get { return _configSelectedEnum; }
            set
            {
                if (SetProperty(ref _configSelectedEnum, value))
                {
                    OnPropertyChanged(nameof(CurrentBatteryNgCriteria));
                    OnPropertyChanged(nameof(CurrentTestOption));
                    SaveSettingChanged("ConfigSelected");
                }
            }
        }

        private BatteryNgCriteria _batteryNgCriteria1;
        private BatteryNgCriteria _batteryNgCriteria2;
        private BatteryNgCriteria _batteryNgCriteria3;
        private BatteryNgCriteria _batteryNgCriteria4;
        private BatteryNgCriteria _batteryNgCriteria5;
        /// <summary>
        /// 电池范围
        /// </summary>
        public BatteryNgCriteria CurrentBatteryNgCriteria
        {
            get
            {
                switch (_configSelectedEnum)
                {
                    case ConfigSelectedEnum.Config1:
                        return _batteryNgCriteria1;
                    case ConfigSelectedEnum.Config2:
                        return _batteryNgCriteria2;
                    case ConfigSelectedEnum.Config3:
                        return _batteryNgCriteria3;
                    case ConfigSelectedEnum.Config4:
                        return _batteryNgCriteria4;
                    case ConfigSelectedEnum.Config5:
                        return _batteryNgCriteria5;
                    default:
                        return _batteryNgCriteria1;
                }
            }
        }
        private Model.TestOption? _testOption1;
        private Model.TestOption? _testOption2;
        private Model.TestOption? _testOption3;
        private Model.TestOption? _testOption4;
        private Model.TestOption? _testOption5;
        /// <summary>
        /// 测试选项
        /// </summary>
        public Model.TestOption? CurrentTestOption
        {
            get
            {
                switch (_configSelectedEnum)
                {
                    case ConfigSelectedEnum.Config1:
                        return _testOption1;
                    case ConfigSelectedEnum.Config2:
                        return _testOption2;
                    case ConfigSelectedEnum.Config3:
                        return _testOption3;
                    case ConfigSelectedEnum.Config4:
                        return _testOption4;
                    case ConfigSelectedEnum.Config5:
                        return _testOption5;
                    default:
                        return _testOption1;
                }
            }
        }

        private FacticitySetting? _facticitySetting;

        public FacticitySetting? CurrentFacticitySetting
        {
            get { return _facticitySetting; }
            set { _facticitySetting = value; }
        }


        public string? PlcSettingJson { get; set; }
        public string? OhmSettingJson { get; set; }
        public string? DmmSettingJson { get; set; }
        public string? SwitchBoardSettingJson { get; set; }
        /// <summary>
        /// Plc缓存地址Json
        /// </summary>
        public string? PlcCacheJsonValue { get; }
        public string? PlcAlarmCacheJsonValue { get; }

        private OperateResult SaveSettingChanged(string name)
        {
            string? settingName = null;
            string? settingValue = null;
            switch (name)
            {
                case "TestOption":
                    {
                        settingName = _testOptionSettingName1;
                        switch (CurrentConfigSelectedEnum)
                        {
                            case ConfigSelectedEnum.Config1:
                                settingName = _testOptionSettingName1;
                                break;
                            case ConfigSelectedEnum.Config2:
                                settingName = _testOptionSettingName2;
                                break;
                            case ConfigSelectedEnum.Config3:
                                settingName = _testOptionSettingName3;
                                break;
                            case ConfigSelectedEnum.Config4:
                                settingName = _testOptionSettingName4;
                                break;
                            case ConfigSelectedEnum.Config5:
                                settingName = _testOptionSettingName5;
                                break;
                            default:
                                settingName = _testOptionSettingName1;
                                break;
                        }
                        settingValue = JsonConvert.SerializeObject(CurrentTestOption);
                        break;
                    }
                case "BatteryNgCriteria":
                    {
                        //settingName = _batteryNgCriteria1SettingName;
                        switch (CurrentConfigSelectedEnum)
                        {
                            case ConfigSelectedEnum.Config1:
                                settingName = _batteryNgCriteriaSettingName1;
                                break;
                            case ConfigSelectedEnum.Config2:
                                settingName = _batteryNgCriteriaSettingName2;
                                break;
                            case ConfigSelectedEnum.Config3:
                                settingName = _batteryNgCriteriaSettingName3;
                                break;
                            case ConfigSelectedEnum.Config4:
                                settingName = _batteryNgCriteriaSettingName4;
                                break;
                            case ConfigSelectedEnum.Config5:
                                settingName = _batteryNgCriteriaSettingName5;
                                break;
                            default:
                                settingName = _batteryNgCriteriaSettingName1;
                                break;
                        }
                        settingValue = JsonConvert.SerializeObject(CurrentBatteryNgCriteria);
                        break;
                    }
                case "CalibrationSetting":
                    {
                        settingName = _calibrationSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentCalibrationSetting);
                        break;
                    }
                case "ConfigSelected":
                    {
                        settingName = _configSelected;
                        settingValue = CurrentConfigSelectedEnum.ToString();
                        break;
                    }
            }
            if (string.IsNullOrEmpty(settingName))
            {
                return OperateResult.CreateFailedResult($"未找到\"{name}\"对应的配置项");
            }
            else
            {
                using (var context = new OcvSettingDbContext())
                {
                    var item = context.SettingItems.FirstOrDefault(x => x.SettingName == settingName);
                    if (item != null)
                    {
                        item.JsonValue = settingValue ?? string.Empty;
                        context.Update(item);
                        context.SaveChanges();
                    }
                    else
                        return OperateResult.CreateFailedResult($"未找到\"{settingName}\"对应的配置项");
                }
                return OperateResult.CreateSuccessResult("保存成功");
            }
        }

    }
}
