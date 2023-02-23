using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works;
using Com.RePower.Ocv.Project.CZD01.BaseProject.DbContext;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles;
using Com.RePower.Ocv.Ui.UiBase;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore;
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
            serviceCollection.AddDbContext<LocalTestResultDbContext>();
            serviceCollection.AddHttpClient();
            string sceneConnectStr = SettingManager.Instance.SceneConnectString ?? string.Empty;
            if(!string.IsNullOrEmpty(sceneConnectStr))
            {
                serviceCollection.AddDbContext<OcvSceneContext>();
            }
            serviceCollection.AddAutoMapper(typeof(OrganizationProfile), typeof(SettingForCZD01Profile));
        }

        protected override void IocRegister(ContainerBuilder builder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(x => x.Name.EndsWith("ViewModel"));
            var assembly = typeof(MainWork).Assembly;
            builder.RegisterAssemblyModules(assembly);
        }
        protected override void OnInitComplate()
        {
            var sceneContext = IocHelper.Default.GetService<OcvSceneContext>();
            if(sceneContext is { })
                sceneContext.Database.EnsureCreated();
            //var localContext = IocHelper.Default.GetService<LocalTestResultDbContext>();
            //if(localContext is { })
            //    localContext.Database.EnsureCreated();
        }
    }
}
