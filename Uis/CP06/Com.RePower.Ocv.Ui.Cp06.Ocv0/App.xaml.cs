﻿using Autofac;
using Com.RePower.Ocv.Ui.UiBase;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0
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
        }
    }
}
