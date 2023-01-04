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

namespace Com.RePower.Ocv.Ui.YiWei
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
                serviceCollection.AddHttpClient();
                serviceCollection.AddDbContext<LocalTestResultDbContext>();
                var autofacServiceProviderFactory = new AutofacServiceProviderFactory();
                var builder = autofacServiceProviderFactory.CreateBuilder(serviceCollection);
                IocRegister(builder);
                var serviceProvider = autofacServiceProviderFactory.CreateServiceProvider(builder);
                IocHelper.Default.ConfigureServices(serviceProvider);
                #endregion
                #region 创建本地存储数据库
                var localDb = IocHelper.Default.GetService<LocalTestResultDbContext>();
                localDb?.Database.EnsureCreated(); 
                #endregion
                #region 初始化UiLog
                LogHelper.RegisterUiLogEvent(new System.Action<object?, log4net.Core.LoggingEvent>((sender, e) =>
                {
                    if (UiLogViewModel.LogSource.Count <= 0 || UiLogViewModel.LogSource.Last().RenderedMessage != e.RenderedMessage)
                    {
                        if (UiLogViewModel.LogSource.Count > 999)
                        {
                            UiLogViewModel.LogSource.RemoveAt(0);
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        UiLogViewModel.LogSource.Add(e));
                    }
                }));
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
            builder.RegisterModule<DMMModule>();//万用表
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<FlowControllerModule>();
            //builder.RegisterModule<NgInfoModule>();
            builder.RegisterModule<TrayModule>();
            builder.RegisterModule<OhmModule>();
            builder.RegisterModule<SwitchBoardModule>();//切换板
            builder.RegisterModule<TestOptionModule>();
            builder.RegisterModule<BatteryNgCriteriaModule>();
            builder.RegisterModule<ProjectModule>();
            builder.RegisterModule<WmsModule>();
        }
    }
}
