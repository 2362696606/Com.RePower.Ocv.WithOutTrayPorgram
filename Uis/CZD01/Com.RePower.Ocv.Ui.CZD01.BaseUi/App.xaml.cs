using Autofac;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works;
using Com.RePower.Ocv.Ui.UiBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : UiBaseApplication
    {
        protected override void AddService(ServiceCollection serviceCollection)
        {
        }

        protected override void IocRegister(ContainerBuilder builder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(x => x.Name.EndsWith("ViewModel"));
            var assembly = typeof(MainWork).Assembly;
            builder.RegisterAssemblyModules(assembly);
        }
    }
}
