using Autofac;
using Autofac.Extensions.DependencyInjection;
using Com.RePower.Ocv.Model;
using Com.RePower.WpfBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
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
        private void IocRegister(ContainerBuilder builder)
        {
            
        }
    }
}
