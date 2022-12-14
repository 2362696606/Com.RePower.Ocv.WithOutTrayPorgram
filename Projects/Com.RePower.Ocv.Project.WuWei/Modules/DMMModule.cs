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

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class DMMModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "万用表");
                if (localPlcSettingObj != null)
                {
                    var localPlcSettingJson = localPlcSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(localPlcSettingJson))
                    {
                        var obj = JsonConvert.DeserializeObject<Keysight_34461A>(localPlcSettingJson);
                        if (obj != null)
                        {
                            builder.RegisterInstance<Keysight_34461A>(obj)
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
