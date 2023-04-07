using Autofac;
using Com.RePower.Ocv.Project.Byd.CB09.Works;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class WorkModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWork>().As<IProjectMainWork>().SingleInstance();
    }
}