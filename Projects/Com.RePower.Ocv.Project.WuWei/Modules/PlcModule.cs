using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Decorators;
using Com.RePower.Ocv.Project.WuWei.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class PlcModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using(OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "本地Plc");
                if(localPlcSettingObj!=null)
                {
                    var localPlcSettingJson = localPlcSettingObj.JsonValue;
                    if(!string.IsNullOrEmpty(localPlcSettingJson))
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
                                .As<IDevice>()
                                .Keyed<IPlc>(PlcDeviceEnum.LocalPlc);
                        }
                        //var obj = JsonConvert.DeserializeObject<PlcNetSimulator>(localPlcSettingJson);
                        //if (obj != null)
                        //{
                        //    builder.RegisterInstance<PlcNetSimulator>(obj)
                        //        .AsSelf()
                        //        .As<IPlc>()
                        //        .As<IDevice>()
                        //        .Keyed<IPlc>(PlcDeviceEnum.LocalPlc);
                        //}
                    }
                }
                //var logisticsPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "物流Plc");
                //if (logisticsPlcSettingObj != null)
                //{
                //    var logisticsPlcSettingJson = logisticsPlcSettingObj.JsonValue;
                //    if (!string.IsNullOrEmpty(logisticsPlcSettingJson))
                //    {
                //        var obj = JsonConvert.DeserializeObject<Siemens_S1500Impl>(logisticsPlcSettingJson);
                //        if (obj != null)
                //        {
                //            builder.RegisterInstance<Siemens_S1500Impl>(obj)
                //                .AsSelf()
                //                .As<IPlc>()
                //                .As<IDevice>()
                //                .Keyed<IPlc>(PlcDeviceEnum.LogisticsPlc);
                //        }
                //    }
                //}
            }
            builder.RegisterDecorator<PlcDecorator, IPlc>();
        }
    }
}
