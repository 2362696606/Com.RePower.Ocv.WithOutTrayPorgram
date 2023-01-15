using Autofac;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class SettingManagerModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingManager>().SingleInstance();
        }
    }
}
