using Autofac;
using Com.RePower.Ocv.Ui.UiBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using Com.RePower.Ocv.Project.Byd.CB09.Works;
using Com.RePower.Ocv.Model.Mapper;

namespace Com.RePower.Ocv.Ui.Byd.CB09
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : UiBaseApplication
    {
        protected override void AddService(ServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddAutoMapper(typeof(OrganizationProfile));
        }

        protected override void IocRegister(ContainerBuilder builder)
        {
            //builder.RegisterModule<WorkModule>();
            var assembly = typeof(MainWork).Assembly;
            builder.RegisterAssemblyModules(assembly);
        }

        protected override void OnInitComplete()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}
