using Autofac;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Mapper;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Modules;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes;
using Com.RePower.Ocv.Ui.UiBase;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Com.RePower.Ocv.Ui.Byd.CB15
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : UiBaseApplication
    {
        public App():base(mainWindowsName: "瑞能OCV")
        {

        }
        protected override void AddService(ServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(OrganizationProfile),typeof(MesDtoProfile));
            //serviceCollection.AddHttpClient();
            serviceCollection.AddDbContext<LocalTestResultDbContext>();
            serviceCollection.AddDbContext<MesDbContext>(option => option.UseSqlServer(connectionString: SettingManager.Instance.CurrentMesSetting?.DbConnectString 
                ?? "Data Source=172.22.65.199;Initial Catalog=CB15_OCV;User Id=repower;Password=Admin@123;TrustServerCertificate=true;"));
        }

        protected override void IocRegister(ContainerBuilder builder)
        {
            builder.RegisterModule<DevicesContorllerModule>();
            builder.RegisterModule<DmmModule>();
            builder.RegisterModule<OhmModule>();
            builder.RegisterModule<PlcModule>();
            builder.RegisterModule<SwitchBoardModule>();
            builder.RegisterModule<TempSensorModule>();
            builder.RegisterModule<WorkModule>();
            builder.RegisterModule<TrayModule>();
            //builder.RegisterModule<SettingManagerModule>();
            builder.RegisterModule<WmsModule>();
            builder.RegisterModule<MesModule>();
        }
        protected override void OnInitComplate()
        {
            using (var settingContext = new OcvSettingDbContext())
            {
                var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                bool isReal = facticityManager?.IsRealMes ?? false;
                if (isReal)
                {
                    var context = IocHelper.Default.GetService<MesDbContext>();
                    context?.Database.EnsureCreated();

                    //var creator = context?.GetService<IRelationalDatabaseCreator>();
                    //creator?.CreateTables();
                }
            }
        }
        //protected override void App_LoadComplate(object sender, NavigationEventArgs e)
        //{
        //    using (var settingContext = new OcvSettingDbContext())
        //    {
        //        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
        //        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
        //        bool isReal = facticityManager?.IsRealMes ?? false;
        //        if (isReal)
        //        {
        //            var context = IocHelper.Default.GetService<MesDbContext>();
        //            context?.Database.EnsureCreated();

        //            //var creator = context?.GetService<IRelationalDatabaseCreator>();
        //            //creator?.CreateTables();
        //        }
        //    }
        //}
    }
}
