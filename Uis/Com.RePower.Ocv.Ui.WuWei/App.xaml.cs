using Autofac;
using Autofac.Extensions.DependencyInjection;
using Com.RePower.Ocv.Project.WuWei.Modules;
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

namespace Com.RePower.Ocv.Ui.WuWei
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                #region 初始化IOC容器
                var serviceCollection = new ServiceCollection();
                var autofacServiceProviderFactory = new AutofacServiceProviderFactory();
                var builder = autofacServiceProviderFactory.CreateBuilder(serviceCollection);
                IocRegister(builder);
                var serviceProvider = autofacServiceProviderFactory.CreateServiceProvider(builder);
                IocHelper.Default.ConfigureServices(serviceProvider);
                #endregion
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "启动异常", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        private void IocRegister(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess).Where(t => t.Name.EndsWith("ViewModel") || t.Name.EndsWith("View"))
                .AsSelf();

            builder.RegisterModule<DevicesControllerModule>();
            builder.RegisterModule<DMMModule>();
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<FlowControllerModule>();
        }
    }
}
