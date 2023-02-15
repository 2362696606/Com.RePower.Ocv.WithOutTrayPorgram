using Autofac;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class WmsServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if(SettingManager.Instance.CurrentFacticity?.IsRealWms??false)
            {
                builder.RegisterType<WmsImpl>()
                    .As<IWmsService>();
            }
            else
            {
                builder.RegisterType<WmsSimulator>()
                    .As<IWmsService>();
            }
        }
    }
}
