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
using Com.RePower.Ocv.Project.WuWei.Controllers;
using Autofac.Features.AttributeFilters;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class DevicesControllerModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                Dictionary<string,string> localPlcAddressCache = new Dictionary<string,string>();
                Dictionary<string,string> logisticsPlcAddressCache = new Dictionary<string,string>();
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
                //var logisticsPlcAddressCacheSettingObj = settingContext.SettingItems.First(x => x.SettingName == "物流Plc缓存");
                //if (logisticsPlcAddressCacheSettingObj != null)
                //{
                //    var logisticsPlcAddressCacheSettingJson = logisticsPlcAddressCacheSettingObj.JsonValue;
                //    if (!string.IsNullOrEmpty(logisticsPlcAddressCacheSettingJson))
                //    {
                //        JArray logisticsPlcAddressCacheSettingArray = JArray.Parse(logisticsPlcAddressCacheSettingJson);
                //        foreach (var item in logisticsPlcAddressCacheSettingArray)
                //        {
                //            var name = item.Value<string>("Name");
                //            var address = item.Value<string>("Address");
                //            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                //            {
                //                logisticsPlcAddressCache.Add(name, address);
                //            }
                //        }
                //    }
                //}
                builder.RegisterType<DevicesController>()
                    .AsSelf()
                    .WithAttributeFiltering()
                    .WithProperty("LocalPlcAddressCache", localPlcAddressCache);
                    //.WithProperty("LogisticsPlcAddressCache", logisticsPlcAddressCache);
            }
        }
    }
}
