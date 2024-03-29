﻿using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.WuWei.Controllers;
using Com.RePower.Ocv.Project.WuWei.Modules;
using Com.RePower.Ocv.Ui.WuWei.ViewModels;
using Com.RePower.Ocv.Ui.WuWei.Views;
using Com.RePower.WpfBase;
using MaterialDesignThemes.Wpf;
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
                serviceCollection.AddHttpClient();
                var autofacServiceProviderFactory = new AutofacServiceProviderFactory();
                var builder = autofacServiceProviderFactory.CreateBuilder(serviceCollection);
                IocRegister(builder);
                var serviceProvider = autofacServiceProviderFactory.CreateServiceProvider(builder);
                IocHelper.Default.ConfigureServices(serviceProvider);
                #endregion
                #region 初始化UiLog
                LogHelper.RegisterUiLogEvent(new System.Action<object?, log4net.Core.LoggingEvent>((sender, e) =>
                        {
                            if (UiLogViewModel.LogSource.Count <= 0 || UiLogViewModel.LogSource.Last().RenderedMessage != e.RenderedMessage)
                            {
                                Application.Current.Dispatcher.Invoke(() => 
                                {
                                    if (UiLogViewModel.LogSource.Count > 999)
                                    {
                                        UiLogViewModel.LogSource.RemoveAt(0);
                                    }
                                    UiLogViewModel.LogSource.Add(e);
                                });
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
            builder.RegisterType<SnackbarMessageQueue>().As<ISnackbarMessageQueue>();

            builder.RegisterModule<DevicesControllerModule>();
            builder.RegisterModule<DmmModule>();
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<FlowControllerModule>();
            //builder.RegisterModule<NgInfoModule>();
            builder.RegisterModule<TrayModule>();
            builder.RegisterModule<SwitchBoardModul>();
            builder.RegisterModule<TestOptionModule>();
            //builder.RegisterModule<BatteryNgCriteriaModule>();
            builder.RegisterModule<ProjectModule>();
            builder.RegisterModule<WmsModule>();
        }
    }
}
