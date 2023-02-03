using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Project.Byd.CB15.Modules;
using Com.RePower.Ocv.Ui.UiBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.Byd.CB15
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : UiBaseApplication
    {
        protected override void AddService(ServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(OrganizationProfile));
            //serviceCollection.AddHttpClient();
            serviceCollection.AddDbContext<LocalTestResultDbContext>();
        }

        protected override void IocRegister(ContainerBuilder builder)
        {
            builder.RegisterModule<DevicesContorllerModules>();
            builder.RegisterModule<DmmModule>();
            builder.RegisterModule<OhmModule>();
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<SwitchBoardModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<TrayModule>();
            builder.RegisterModule<SettingManagerModule>();
            builder.RegisterModule<WmsModule>();
        }
    }
}
