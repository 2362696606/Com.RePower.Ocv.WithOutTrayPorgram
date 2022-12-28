using Autofac;
using Com.RePower.Ocv.Project.YiWei.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class WorkModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWorkFixed>()
                .AsSelf()
                .As<IProjectMainWork>();
        }
    }
}
