using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
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
        private readonly string plcSettingName = "PlcSetting";
        private readonly string dmmSettingName = "DmmSetting";
        private readonly string ohmSettingName = "OhmSetting";
        private readonly string switchBoardSettingName = "SwitchBoardSetting";

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
                    var dto = JsonConvert.DeserializeObject<TestOptionDto>(testOptionJStr);
                    this._testOption = mapper.Map<TestOption>(dto);
                }
                #endregion
                #region 初始化BatteryStandard
                var batteryStandardJStr = context.SettingItems.FirstOrDefault(x => x.SettingName == batteryStandardSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(batteryStandardJStr))
                {
                    var dto = JsonConvert.DeserializeObject<BatteryStandardSettingDto>(batteryStandardJStr);
                    this._batteryStandard = mapper.Map<BatteryStandard>(dto);
                }
                #endregion
                #region 初始化硬件配置字符串
                PlcSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == plcSettingName)?.JsonValue;
                OhmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == ohmSettingName)?.JsonValue;
                DmmSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == dmmSettingName)?.JsonValue;
                SwitchBoardSettingJson = context.SettingItems.FirstOrDefault(x => x.SettingName == switchBoardSettingName)?.JsonValue; 
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

        private TestOption? _testOption;
        /// <summary>
        /// 当前TestOption
        /// </summary>
        public TestOption? CurrentTestOption
        {
            get { return _testOption; }
        }
        private BatteryStandard? _batteryStandard;
        /// <summary>
        /// 当前BatteryStandard
        /// </summary>
        public BatteryStandard? CurrentBatteryStandard
        {
            get { return _batteryStandard; }
        }
        public string? PlcSettingJson { get; set; }
        public string? OhmSettingJson { get; set; }
        public string? DmmSettingJson { get; set; }
        public string? SwitchBoardSettingJson { get; set; }
    }
}
