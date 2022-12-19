using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class TestOptionModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var testOptionSettingObj = settingContext.SettingItems.First(x => x.SettingName == "测试选项");
                if (testOptionSettingObj != null)
                {
                    var testOptionSettingJson = testOptionSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(testOptionSettingJson))
                    {
                        var obj = JsonConvert.DeserializeObject<TestOption>(testOptionSettingJson);
                        if(obj!=null)
                        {
                            builder.RegisterInstance(obj);
                        }
                    }
                }
            }
        }
    }
}
