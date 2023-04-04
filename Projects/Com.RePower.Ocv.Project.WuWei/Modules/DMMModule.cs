using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class DmmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var dmmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "万用表");
                if (dmmSettingObj != null)
                {
                    var dmmSettingJson = dmmSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(dmmSettingJson))
                    {
                        JObject jObj = JObject.Parse(dmmSettingJson);
                        bool isReal = jObj.Value<bool>("IsReal");
                        IDmm? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<Keysight34461AImpl>(dmmSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<Keysight34461ASimulator>(dmmSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .As<IDmm>()
                                .As<IDevice>();
                        }
                    }
                }
            }
        }
    }
}
