using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Model;
using Com.RePower.Ocv.Project.WuWei.Serivces;
using Com.RePower.Ocv.Project.WuWei.Serivces.Impl;
using Com.RePower.Ocv.Project.WuWei.Serivces.Module;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class WmsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var wmsItem = settingContext.SettingItems.First(x => x.SettingName == "Wms");
                string wmsJsonStr = wmsItem.JsonValue;
                if (!string.IsNullOrEmpty(wmsJsonStr))
                {
                    var obj = JsonConvert.DeserializeObject<WmsSetting>(wmsJsonStr);
                    if (obj != null)
                    {
                        builder.RegisterInstance(obj);
                    }
                    var jObj = JObject.Parse(wmsJsonStr);
                    bool isReal = jObj.Value<bool>("IsReal");
                    if (isReal)
                    {
                        builder.RegisterType<WmsService>()
                            .AsSelf()
                            .As<IWmsService>();
                    }
                    else
                    {
                        builder.RegisterType<WmsSimulator>()
                            .AsSelf()
                            .As<IWmsService>();
                    }

                }
            }
        }
    }
}
