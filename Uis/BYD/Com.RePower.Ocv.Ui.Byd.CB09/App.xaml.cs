using Autofac;
using Com.RePower.Ocv.Ui.UiBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Modules;
using System.Reflection;

namespace Com.RePower.Ocv.Ui.Byd.CB09
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
            builder.RegisterModule<WorkModule>();
        }

        protected override void OnInitComplete()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }
}
