using Autofac;
using Com.RePower.Ocv.Project.WuWei.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class FlowControllerModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FlowController>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
