using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Model;
using Com.RePower.WpfBase;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public class SettingManager
    {
        private readonly string _testOptionSettingName = "测试选项";
        private readonly string _batteryNgCriteriaSettingName = "电池标准";

        private static readonly Lazy<SettingManager> _instance =
           new Lazy<SettingManager>(() => new SettingManager());

        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static SettingManager Instance
        { get { return _instance.Value; } }

        public SettingManager()
        {
            using var settingContext = new OcvSettingDbContext();

            #region 初始化电池范围

            var ngCriteriaJsonStr = settingContext.SettingItems.First(x => x.SettingName == _batteryNgCriteriaSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(ngCriteriaJsonStr))
            {
                _batteryNgCriteria = JsonConvert.DeserializeObject<BatteryNgCriteria>(ngCriteriaJsonStr);
                if (_batteryNgCriteria is { })
                    _batteryNgCriteria.SaveAction = SaveSettingChanged;
            }

            #endregion 初始化电池范围

            #region 初始化TestOption

            var testOptionSettingJson = settingContext.SettingItems.First(x => x.SettingName == _testOptionSettingName)?.JsonValue;
            if (!string.IsNullOrEmpty(testOptionSettingJson))
            {
                _testOption = JsonConvert.DeserializeObject<TestOption>(testOptionSettingJson);
                if (_testOption is { })
                    _testOption.SaveAction = SaveSettingChanged;
            }

            #endregion 初始化TestOption
        }

        private readonly BatteryNgCriteria? _batteryNgCriteria;

        public BatteryNgCriteria? BatteryNgCriteria
        {
            get { return _batteryNgCriteria; }
        }

        private TestOption? _testOption;

        public TestOption? TestOption
        {
            get { return _testOption; }
            set { _testOption = value; }
        }

        private OperateResult SaveSettingChanged(string name)
        {
            string? settingName = null;
            string? settingValue = null;
            switch (name)
            {
                case "TestOption":
                    {
                        settingName = _testOptionSettingName;
                        settingValue = JsonConvert.SerializeObject(TestOption);
                        break;
                    }
                case "BatteryNgCriteria":
                    {
                        settingName = _batteryNgCriteriaSettingName;
                        settingValue = JsonConvert.SerializeObject(BatteryNgCriteria);
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
                }
                return OperateResult.CreateSuccessResult("保存成功");
            }
        }
    }
}