using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class WorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (var settingContext = new OcvSettingDbContext())
            {
                var item = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                string value = item.JsonValue;
                OcvTypeEnmu ocvType = Enum.Parse<OcvTypeEnmu>(value);
                switch(ocvType)
                {
                    case OcvTypeEnmu.OCV3:
                        builder.RegisterType<MainWorkForOcv3>()
                            .AsSelf()
                            .As<IProjectMainWork>();
                        break;
                    default:
                        builder.RegisterType<MainWork>()
                            .AsSelf()
                            .As<IProjectMainWork>();
                        break;
                }
            }

        }
    }
}
