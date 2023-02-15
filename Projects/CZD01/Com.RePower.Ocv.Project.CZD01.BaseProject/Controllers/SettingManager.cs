using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers
{
    public class SettingManager : ObservableObject
    {
        private readonly string defaultOcvTypeSettingName = "DefaultOcvType";
        private readonly string facticitySettingName = "FacticitySetting";
        private readonly string testOptionSettingName = "TestOptionSetting";
        private readonly string batteryStandardSettingName = "BatteryStandardSetting";
        private readonly string wmsSettingName = "WmsSetting";
        private readonly string testOrderSettingName = "TestOrder";
        private readonly string plcSettingName = "PlcSetting";
        private readonly string dmmSettingName = "DmmSetting";
        private readonly string ohmSettingName = "OhmSetting";
        private readonly string switchBoardSettingName = "SwitchBoardSetting";
        private readonly string temperatureSensorSettingName = "TemperatureSetting";
        private readonly string plcAddressCacheSettingName = "PlcAddressCache";
        private readonly string otherSettingName = "OtherSetting";
        private readonly string sceneConnectStringSettingName = "OcvConnectString";

        private IMapper mapper;
        
        private static readonly Lazy<SettingManager> _instance =
           new Lazy<SettingManager>(() => new SettingManager());
        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static SettingManager Instance { get { return _instance.Value; } }

        private SettingManager() 
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<SettingProfile>();
                cfg.AddProfile<SettingForCZD01Profile>();
            });
            this.mapper = configuration.CreateMapper();
            using (OcvSettingDbContext context = new OcvSettingDbContext())
            {
                #region 初始化OcvType
                var ocvTypeJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == defaultOcvTypeSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(ocvTypeJStr))
                {
                    this._ocvType = Enum.Parse<OcvTypeEnum>(ocvTypeJStr);
                }
                #endregion
                #region 初始化设备真实性信息
                var facticityJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == facticitySettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(facticityJStr))
                {
                    var dto = JsonConvert.DeserializeObject<FacticitySettingDto>(facticityJStr);
                    this._facticitySetting = mapper.Map<FacticitySetting>(dto);
                } 
                #endregion
                #region 初始化TestOption
                var testOptionJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == testOptionSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(testOptionJStr))
                {
                    var dto = JsonConvert.DeserializeObject<Settings.Dtos.TestOptionDto>(testOptionJStr);
                    this._testOption = mapper.Map<Settings.TestOption>(dto);
                }
                #endregion
                #region 初始化BatteryStandard
                var batteryStandardJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == batteryStandardSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(batteryStandardJStr))
                {
                    var dto = JsonConvert.DeserializeObject<Settings.Dtos.BatteryStandardSettingDto>(batteryStandardJStr);
                    this._batteryStandard = mapper.Map<Settings.BatteryStandard>(dto);
                }
                #endregion
                #region 初始化WmsSetting
                var wmsJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == wmsSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(wmsJStr))
                {
                    var dto = JsonConvert.DeserializeObject<WmsSettingDto>(wmsJStr);
                    this._wmsSetting = mapper.Map<WmsSetting>(dto);
                }
                #endregion
                #region 初始化TestOrder
                var testOrderJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == testOrderSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(testOrderJStr))
                {
                    this._testOrder = JsonConvert.DeserializeObject<List<List<int>>>(testOrderJStr);
                }
                #endregion
                #region 初始化OtherSetting
                var otherSettingJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == otherSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(otherSettingJStr))
                {
                    var dto = JsonConvert.DeserializeObject<OtherSettingDto>(otherSettingJStr);
                    this._otherSetting = mapper.Map<OtherSetting>(dto);
                }
                #endregion

                #region 初始化PlcAddressCache
                var plcAddressCacheJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == plcAddressCacheSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(plcAddressCacheJStr))
                {
                    this._plcValueCacheSetting = new PlcValueCacheSetting();
                    //this._plcValueCacheSetting.PlcAddressCache = new List<Model.Entity.PlcCacheValue>();
                    this._plcValueCacheSetting.PlcAddressCache = JsonConvert.DeserializeObject<List<Model.Entity.PlcCacheValue>>(plcAddressCacheJStr)
                        ?? new List<Model.Entity.PlcCacheValue>();
                }
                #endregion

                #region 初始化硬件配置字符串
                PlcSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == plcSettingName)?.JsonValue;
                OhmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == ohmSettingName)?.JsonValue;
                DmmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == dmmSettingName)?.JsonValue;
                SwitchBoardSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == switchBoardSettingName)?.JsonValue;
                TemperatureSensorJson = context.SettingItems.FirstOrDefault(x => x.SettingName == temperatureSensorSettingName)?.JsonValue;
                #endregion
                #region 获取数据库连接字符串
                SceneConnectString = context.SettingItems.FirstOrDefault(x => x.SettingName == sceneConnectStringSettingName)?.JsonValue; 
                #endregion
            }
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
        private FacticitySetting? _facticitySetting;
        /// <summary>
        /// 当前设备真实性信息
        /// </summary>
        public FacticitySetting? CurrentFacticity
        {
            get { return _facticitySetting; }
        }

        private Settings.TestOption? _testOption;
        /// <summary>
        /// 当前TestOption
        /// </summary>
        public Settings.TestOption? CurrentTestOption
        {
            get { return _testOption; }
        }
        private Settings.BatteryStandard? _batteryStandard;
        /// <summary>
        /// 当前BatteryStandard
        /// </summary>
        public Settings.BatteryStandard? CurrentBatteryStandard
        {
            get { return _batteryStandard; }
        }
        private WmsSetting? _wmsSetting;
        /// <summary>
        /// 当前WmsSetting
        /// </summary>
        public WmsSetting? CurrentWmsSetting
        {
            get { return _wmsSetting; }
        }
        private List<List<int>>? _testOrder;

        public List<List<int>>? CurrentTestOrder
        {
            get { return _testOrder; }
        }
        private PlcValueCacheSetting? _plcValueCacheSetting;

        public PlcValueCacheSetting? PlcValueCacheSetting
        {
            get { return _plcValueCacheSetting; }
        }
        private OtherSetting? _otherSetting;

        public OtherSetting? CurrentOtherSetting
        {
            get { return _otherSetting; }
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
    }
}
