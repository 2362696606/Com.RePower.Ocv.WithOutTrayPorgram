using Autofac;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class DevicesControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DevicesController>()
                .SingleInstance();
        }
    }
}
