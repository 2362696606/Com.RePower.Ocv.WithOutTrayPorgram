using Autofac;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.DeviceBase.DMM;
using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class OhmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var ohmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "内阻仪");
                if (ohmSettingObj != null)
                {
                    var ohmSettingJson = ohmSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(ohmSettingJson))
                    {
                        JObject jObj = JObject.Parse(ohmSettingJson);
                        bool isReal = jObj.Value<bool>("IsReal");
                        IOhm? obj = null;
                        if(isReal)
                        {
                            obj = JsonConvert.DeserializeObject<Hioki_BT3562Impl>(ohmSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<Hioki_BT3562Simulator>(ohmSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .As<IOhm>()
                                .As<IDevice>();
                        }
                    }
                }
            }
        }
    }
}
