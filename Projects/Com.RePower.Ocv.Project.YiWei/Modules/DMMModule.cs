using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class DMMModule:Module
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
                        var obj = JsonConvert.DeserializeObject<Keysight_34461AImpl>(dmmSettingJson);
                        //var obj = JsonConvert.DeserializeObject<DmmSimulator>(dmmSettingJson);
                        if (obj != null)
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .As<IDMM>()
                                .As<IDevice>();
                        }
                    }
                }
            }
        }
    }
}
