using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Wms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class WmsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (var settingContext = new OcvSettingDbContext())
            {

                var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                bool isReal = facticityManager?.IsRealWms ?? false;
                if (isReal)
                {
                    builder.RegisterType<WmsSimulator>()
                        .AsSelf()
                        .As<IWmsService>();

                    builder.RegisterType<WmsImpl>()
                        .AsSelf()
                        .As<IWmsService>();
                }
                else
                {
                    builder.RegisterType<WmsImpl>()
                        .AsSelf()
                        .As<IWmsService>();

                    builder.RegisterType<WmsSimulator>()
                        .AsSelf()
                        .As<IWmsService>();
                }

            }
        }
    }
}
