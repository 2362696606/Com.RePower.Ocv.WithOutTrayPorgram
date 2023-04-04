using Autofac;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Com.RePower.Ocv.Project.YiWei.Controllers;
using Autofac.Features.AttributeFilters;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class DevicesControllerModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                Dictionary<string,string> localPlcAddressCache = new Dictionary<string,string>();
                var localPlcAddressCacheSettingObj = settingContext.SettingItems.First(x => x.SettingName == "本地Plc缓存");
                if (localPlcAddressCacheSettingObj != null)
                {
                    var localPlcAddressCacheSettingJson = localPlcAddressCacheSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(localPlcAddressCacheSettingJson))
                    {
                        JArray localPlcAddressCacheSettingArray = JArray.Parse(localPlcAddressCacheSettingJson);
                         foreach(var item in localPlcAddressCacheSettingArray)
                        {
                            var name = item.Value<string>("Name");
                            var address = item.Value<string>("Address");
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                            {
                                localPlcAddressCache.Add(name, address);
                            }
                        }
                    }
                }

                Dictionary<string, string> plcAlarmAddressCache = new();
                var plcAlarmCacheJsonValue = SettingManager<SettingManager>.Instance.PlcAlarmCacheJsonValue;
                if (!string.IsNullOrEmpty(plcAlarmCacheJsonValue))
                {
                    var plcAlarmAddressCacheSettingArray = JArray.Parse(plcAlarmCacheJsonValue);
                    foreach (var item in plcAlarmAddressCacheSettingArray)
                    {
                        var name = item.Value<string>("Name");
                        var address = item.Value<string>("Address");
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                        {
                            plcAlarmAddressCache.Add(name,address);
                        }
                    }
                }

                builder.RegisterType<DevicesController>()
                    .AsSelf()
                    .WithAttributeFiltering()
                    .WithProperty("LocalPlcAddressCache", localPlcAddressCache)
                    .WithProperty("PlcAlarmAddressCache", plcAlarmAddressCache);
                //.WithProperty("LogisticsPlcAddressCache", logisticsPlcAddressCache);
            }
        }
    }
}
