using Autofac;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class WmsModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var authenticitySetting = new AuthenticitySetting();
        if(authenticitySetting.IsRealWms)
        {
            builder.RegisterType<WmsService>().As<IWmsService>();
        }
        else
        {
            builder.RegisterType<WmsSimulator>().As<IWmsService>();
        }
    }
}