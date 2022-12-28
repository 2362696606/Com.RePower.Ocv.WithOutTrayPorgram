using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Decorators;
using Com.RePower.Ocv.Project.YiWei.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class PlcModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using(OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                //var localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "本地Plc");
                //if(localPlcSettingObj!=null)
                //{
                //    var localPlcSettingJson = localPlcSettingObj.JsonValue;
                //    if(!string.IsNullOrEmpty(localPlcSettingJson))
                //    {
                //        var obj = JsonConvert.DeserializeObject<InovanceTcpNetPlcImpl>(localPlcSettingJson);
                //        if(obj!=null)
                //        {
                //            builder.RegisterInstance<InovanceTcpNetPlcImpl>(obj)
                //                .AsSelf()
                //                .As<IPlc>()
                //                .As<IDevice>()
                //                .Keyed<IPlc>(PlcDeviceEnum.LocalPlc);
                //        }
                //    }
                //}
                var logisticsPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "本地Plc");
                if (logisticsPlcSettingObj != null)
                {
                    var logisticsPlcSettingJson = logisticsPlcSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(logisticsPlcSettingJson))
                    {
                        var obj = JsonConvert.DeserializeObject<Siemens_S1500Impl>(logisticsPlcSettingJson);
                        if (obj != null)
                        {
                            builder.RegisterInstance<Siemens_S1500Impl>(obj)
                                .AsSelf()
                                .As<IPlc>()
                                .As<IDevice>()
                                .Keyed<IPlc>(PlcDeviceEnum.LocalPlc);
                        }
                    }
                }
            }
            builder.RegisterDecorator<PlcDecorator, IPlc>();
        }
    }
}
