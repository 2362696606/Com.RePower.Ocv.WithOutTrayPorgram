﻿using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Model;
using Com.RePower.Ocv.Project.YiWei.Serivces;
using Com.RePower.Ocv.Project.YiWei.Serivces.Impl;
using Com.RePower.Ocv.Project.YiWei.Serivces.Module;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class WmsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var wmsItem = settingContext.SettingItems.First(x => x.SettingName == "Wms");
                string wmsJsonStr = wmsItem.JsonValue;
                if (!string.IsNullOrEmpty(wmsJsonStr))
                {
                    var obj = JsonConvert.DeserializeObject<WmsSetting>(wmsJsonStr);
                    if (obj != null)
                    {
                        builder.RegisterInstance<WmsSetting>(obj);
                    }
                    //var jObj = JToken.Parse(wmsJsonStr);
                    //string getBatteryInfoUrl = jObj.Value<string>("GetBatteryInfoUrl") ??string.Empty;
                    //string uploadTestResultUrl = jObj.Value<string>("UploadTestResultUrl") ??string.Empty;
                    //string baseAddress = jObj.Value<string>("BaseAddress") ??string.Empty;
                    //string equipmentCode = jObj.Value<string>("EquipmentCode") ??string.Empty;
                    builder.RegisterType<WmsService>()
                        .AsSelf()
                        .As<IWmsService>();
                        //.WithProperty("GetBatteryInfoUrl", getBatteryInfoUrl)
                        //.WithProperty("UploadTestResultUrl", uploadTestResultUrl)
                        //.WithProperty("BaseAddress", baseAddress)
                        //.WithProperty("EquipmentCode", equipmentCode);
                        
                }
            }
        }
    }
}