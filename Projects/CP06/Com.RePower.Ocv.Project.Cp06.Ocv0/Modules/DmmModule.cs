using Autofac;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
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
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealDmm ?? false;
                        IDMM? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<Keysight_34461AImpl>(dmmSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<Keysight_34461ASimulator>(dmmSettingJson);
                        }
                        if (obj is { })
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
