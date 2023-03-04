using Autofac.Extensions.DependencyInjection;
using Autofac;
using Com.RePower.WpfBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Ui.YiWei.ViewModels;
using Com.RePower.Ocv.Project.YiWei.Modules;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Ui.UiBase;
using Com.RePower.Ocv.Model.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Com.RePower.Ocv.Ui.YiWei
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : UiBaseApplication
    {
        public App()
        {
            //this.Startup += new StartupEventHandler(App_Startup);
        }

        protected override void AddService(ServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(OrganizationProfile));
            serviceCollection.AddHttpClient();
            serviceCollection.AddDbContext<LocalTestResultDbContext>();
        }


        protected override void IocRegister(ContainerBuilder builder)
        {
            builder.RegisterModule<DevicesControllerModule>();
            builder.RegisterModule<DMMModule>();//万用表
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<FlowControllerModule>();
            //builder.RegisterModule<NgInfoModule>();
            builder.RegisterModule<TrayModule>();
            builder.RegisterModule<OhmModule>();
            builder.RegisterModule<SwitchBoardModule>();//切换板
            //builder.RegisterModule<TestOptionModule>();
            //builder.RegisterModule<BatteryNgCriteriaModule>();
            builder.RegisterModule<ProjectModule>();
            //builder.RegisterModule<WmsModule>();
        }
    }
}
