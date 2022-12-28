using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class BatteryNgCriteriaModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var ngCriteriaItem = settingContext.SettingItems.First(x=>x.SettingName == "电池标准");
                string ngCriteriaJsonStr = ngCriteriaItem.JsonValue;
                if(!string.IsNullOrEmpty(ngCriteriaJsonStr))
                {
                    var obj = JsonConvert.DeserializeObject<BatteryNgCriteria>(ngCriteriaJsonStr);
                    if(obj!=null)
                    {
                        builder.RegisterInstance<BatteryNgCriteria>(obj);
                    }
                }
            }
        }
    }
}
