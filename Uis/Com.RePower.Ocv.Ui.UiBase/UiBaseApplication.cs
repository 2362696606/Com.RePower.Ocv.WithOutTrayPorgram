using Autofac;
using Autofac.Extensions.DependencyInjection;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Ui.UiBase.ViewModels;
using Com.RePower.WpfBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.UiBase
{
    public abstract class UiBaseApplication:Application
    {
        public UiBaseApplication()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri("/Com.RePower.Ocv.Ui.UiBase;component/Views/Dictionarys/BaseDictionary.xaml", UriKind.RelativeOrAbsolute) }
                );
        }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                #region 初始化IOC容器
                var serviceCollection = new ServiceCollection();
                AddService(serviceCollection);
                var autofacServiceProviderFactory = new AutofacServiceProviderFactory();
                var builder = autofacServiceProviderFactory.CreateBuilder(serviceCollection);
                BaseRegister(builder);
                IocRegister(builder);
                var serviceProvider = autofacServiceProviderFactory.CreateServiceProvider(builder);
                IocHelper.Default.ConfigureServices(serviceProvider);
                #endregion
                #region 创建本地存储数据库
                var localDb = IocHelper.Default.GetService<LocalTestResultDbContext>();
                localDb?.Database.EnsureCreated();
                #endregion
                #region 初始化完成后调用
                OnInitComplate(); 
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

        protected abstract void AddService(ServiceCollection serviceCollection);
        protected abstract void IocRegister(ContainerBuilder builder);
        protected virtual void OnInitComplate()
        { }

        private void BaseRegister(ContainerBuilder builder)
        {
            //var dataAccess = Assembly.GetExecutingAssembly();
            var dataAccess = Assembly.GetEntryAssembly();
            if (dataAccess is { })
            {
                builder.RegisterAssemblyTypes(dataAccess)
                    .Where(t => t.Name.EndsWith("ViewModel") || t.Name.EndsWith("View"))
                    .AsSelf();
            }
            builder.RegisterType<UiLogViewModel>();
        }
    }
}
