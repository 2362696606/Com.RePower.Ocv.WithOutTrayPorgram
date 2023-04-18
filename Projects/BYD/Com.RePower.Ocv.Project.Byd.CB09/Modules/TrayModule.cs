using Autofac;
using Com.RePower.Ocv.Model.Entity;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class TrayModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Tray>().AsSelf().SingleInstance();
    }
}