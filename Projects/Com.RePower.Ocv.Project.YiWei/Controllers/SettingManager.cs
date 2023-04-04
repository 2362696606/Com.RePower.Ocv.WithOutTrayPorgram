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
        private readonly string _testOptionSettingName = "测试选项";
        private readonly string _batteryNgCriteriaSettingName = "电池标准";
        private readonly string _facticitySettingName = "FacticitySetting";
        private readonly string _plcSettingName = "本地Plc";
        private readonly string _ohmSettingName = "内阻仪";
        private readonly string _dmmSettingName = "万用表";
        private readonly string _switchBoardSettingName = "切换板";
        private readonly string _plcCacheValueSettingName = "本地Plc缓存";
        private readonly string _plcAlarmCacheValueSettingName = "Plc报警缓存";
        private readonly string _calibrationSettingName = "校准选项";


        private SettingManager()
        {
            using (var dbContext = new OcvSettingDbContext())
            {
                #region 初始化TestOption
                var testOptionSettingJson = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _testOptionSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(testOptionSettingJson))
                {
                    _testOption = JsonConvert.DeserializeObject<Model.TestOption>(testOptionSettingJson);
                    if (_testOption is { }) _testOption.SaveAction = SaveSettingChanged;
                }
                #endregion

                #region 初始化BatteryNgCriteria
                var ngCriteriaJsonStr = dbContext.SettingItems.FirstOrDefault(x => x.SettingName == _batteryNgCriteriaSettingName)?.JsonValue;
                if (!string.IsNullOrEmpty(ngCriteriaJsonStr))
                {
                    _batteryNgCriteria = JsonConvert.DeserializeObject<BatteryNgCriteria>(ngCriteriaJsonStr);
                    if(_batteryNgCriteria is { }) _batteryNgCriteria.SaveAction = SaveSettingChanged;
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
        private Model.CalibrationSetting? _calibrationSetting;

        public Model.CalibrationSetting? CurrentCalibrationSetting
        {
            get { return _calibrationSetting; }
            set { _calibrationSetting = value; }
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
                        settingName = _testOptionSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentTestOption);
                        break;
                    }
                case "BatteryNgCriteria":
                    {
                        settingName = _batteryNgCriteriaSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentBatteryNgCriteria);
                        break;
                    }
                case "CalibrationSetting":
                    {
                        settingName = _calibrationSettingName;
                        settingValue = JsonConvert.SerializeObject(CurrentCalibrationSetting);
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
