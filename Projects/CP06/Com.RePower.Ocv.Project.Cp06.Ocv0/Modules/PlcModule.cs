using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Decorators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class PlcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "Plc");
                if (localPlcSettingObj != null)
                {
                    var localPlcSettingJson = localPlcSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(localPlcSettingJson))
                    {
                        var jObj = JObject.Parse(localPlcSettingJson);
                        bool isReal = jObj.Value<bool>("IsReal");
                        IPlc? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<InovanceTcpNetPlcImpl>(localPlcSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<PlcNetSimulator>(localPlcSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .As<IPlc>()
                                .As<IDevice>();
                        }
                    }
                }
            }
            builder.RegisterDecorator<PlcDecorator, IPlc>();
        }
    }
}
