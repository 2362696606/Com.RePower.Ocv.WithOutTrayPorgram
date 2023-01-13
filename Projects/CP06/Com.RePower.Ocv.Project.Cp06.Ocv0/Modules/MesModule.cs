using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Wms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class MesModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (var settingContext = new OcvSettingDbContext())
            {
                var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                bool isReal = facticityManager?.IsRealMes ?? false;
                if (isReal)
                {
                    builder.RegisterType<MesImpl>()
                        .AsSelf()
                        .As<IMesService>();
                }
                else
                {
                    builder.RegisterType<MesSimulator>()
                        .AsSelf()
                        .As<IMesService>();
                } 
            }
        }
    }
}
