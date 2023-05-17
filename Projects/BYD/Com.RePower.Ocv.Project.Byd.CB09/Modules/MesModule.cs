using Autofac;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class MesModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        if (AuthenticitySetting.Default.IsRealMes)
        {
            builder.RegisterType<MesService>().As<IMesService>();
        }
        else
        {
            builder.RegisterType<MesSimulator>().As<IMesService>();
        }
    }
}