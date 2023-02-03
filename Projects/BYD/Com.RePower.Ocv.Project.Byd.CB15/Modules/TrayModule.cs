using Autofac;
using Com.RePower.Ocv.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class TrayModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Tray>()
                .SingleInstance();
        }
    }
}
