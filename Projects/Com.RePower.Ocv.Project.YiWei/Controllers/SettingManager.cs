using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Project.YiWei.Model;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Controllers
{
    public class SettingManager
    {
        private readonly string testOptionSettingName = "测试选项";
        private readonly string batteryNgCriteriaSettingName = "电池标准";
        private readonly string facticitySettingName = "FacticitySetting";
        private readonly string plcSettingName = "本地Plc";
        private readonly string ohmSettingName = "内阻仪";
        private readonly string dmmSettingName = "万用表";
        private readonly string switchBoardSettingName = "切换板";


        private SettingManager()
        {
            using (var dbContext = new OcvSettingDbContext())
            {
                #region 初始化TestOption
                var testOptionSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == testOptionSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(testOptionSettingJson))
                {
                    _testOption = JsonConvert.DeserializeObject<Model.TestOption>(testOptionSettingJson);
                    if (_testOption is { }) _testOption.SaveAction = SaveSettingChanged;
                }
                #endregion

                #region 初始化BatteryNgCriteria
                var ngCriteriaJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == batteryNgCriteriaSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(ngCriteriaJsonStr))
                {
                    _batteryNgCriteria = JsonConvert.DeserializeObject<BatteryNgCriteria>(ngCriteriaJsonStr);
                    if(_batteryNgCriteria is { }) _batteryNgCriteria.SaveAction = SaveSettingChanged;
                }
                #endregion

                #region 设备真实性配置
                var facticitySettingJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == facticitySettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(facticitySettingJsonStr))
                    _facticitySetting = JsonConvert.DeserializeObject<FacticitySetting>(facticitySettingJsonStr); 
                #endregion

                #region 获取硬件配置字符串
                PlcSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == plcSettingName)?.JsonValue;
                OhmSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == ohmSettingName)?.JsonValue;
                DmmSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == dmmSettingName)?.JsonValue;
                SwitchBoardSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == switchBoardSettingName)?.JsonValue; 
                #endregion

            }
        }

        private BatteryNgCriteria? _batteryNgCriteria;
        /// <summary>
        /// 电池范围
        /// </summary>
        public BatteryNgCriteria? CurrentBatteryNgCriteria
        {
            get { return _batteryNgCriteria; }
            set { _batteryNgCriteria = value; }
        }
        private Model.TestOption? _testOption;
        /// <summary>
        /// 测试选项
        /// </summary>
        public Model.TestOption? CurrentTestOption
        {
            get { return _testOption; }
            set { _testOption = value; }
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

        private OperateResult SaveSettingChanged(string name)
        {
            string? settingName = null;
            string? settingValue = null;
            switch (name)
            {
                case "TestOption":
                    {
                        settingName = testOptionSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentTestOption);
                        break;
                    }
                case "BatteryNgCriteria":
                    {
                        settingName = batteryNgCriteriaSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentBatteryNgCriteria);
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
