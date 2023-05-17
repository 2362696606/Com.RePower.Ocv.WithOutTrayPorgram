using Autofac;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.Ocv.Project.Byd.CB09.Works;

namespace Com.RePower.Ocv.Project.Byd.CB09.Modules;

public class WorkModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        switch (SystemSetting.Default.DefaultWorkType)
        {
            case WorkTypeEnum.FourBars:
            {
                builder.RegisterType<MainWork>().As<IProjectMainWork>().SingleInstance();
                break;
            }
            case WorkTypeEnum.OneBars:
            {
                builder.RegisterType<MainWorkForOneBars>().As<IProjectMainWork>().SingleInstance();
                break;
            }
            default:
            {
                builder.RegisterType<MainWork>().As<IProjectMainWork>().SingleInstance();
                break;
            }
        }
    }
}