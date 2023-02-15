using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Wms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class WmsModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register<WmsSetting>(c =>
            //{
            //    using (var settingContext = new OcvSettingDbContext())
            //    {
            //        var wmsItem = settingContext.SettingItems.First(x => x.SettingName == "Wms");
            //        string wmsJsonStr = wmsItem.JsonValue;
            //        if (!string.IsNullOrEmpty(wmsJsonStr))
            //        {
            //            var obj = JsonConvert.DeserializeObject<WmsSetting>(wmsJsonStr);
            //            if(obj is { })
            //            {
            //                return obj;
            //            }
            //        }
            //        return new WmsSetting();
            //    }
            //});
            using (var settingContext = new OcvSettingDbContext())
            {
                //OcvSettingItemDto? wmsItem = null;

                //wmsItem = settingContext.SettingItems.First(x => x.SettingName == "Wms");
                //string wmsJsonStr = wmsItem.JsonValue;
                //if(!string.IsNullOrEmpty(wmsJsonStr))
                //{
                //    var jObj = JObject.Parse(wmsJsonStr);
                //    bool isReal = jObj.Value<bool>("IsReal");
                //    if(isReal)
                //    {
                //        builder.RegisterType<WmsImpl>()
                //            .AsSelf()
                //            .As<IWmsService>();
                //        return;
                //    }
                //}
                //builder.RegisterType<WmsSimulator>()
                //            .AsSelf()
                //            .As<IWmsService>();

                var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                bool isReal = facticityManager?.IsRealWms ?? false;
                if (isReal)
                {
                    builder.RegisterType<WmsImpl>()
                        .AsSelf()
                        .As<IWmsService>();
                }
                else
                {
                    builder.RegisterType<WmsSimulator>()
                        .AsSelf()
                        .As<IWmsService>();
                }
            }
        }
    }
}
