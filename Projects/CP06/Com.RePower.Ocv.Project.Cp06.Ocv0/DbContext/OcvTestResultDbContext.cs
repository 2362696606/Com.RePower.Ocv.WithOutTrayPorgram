using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.DbContext
{
    public class OcvTestResultDbContext : LocalTestResultDbContext
    {
        //public DbSet<BatteryDto> Batterys { get; set; }
        //public DbSet<NgInfoDto> NgInfos { get; set; }
        //public DbSet<TrayDto> Trays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectStr;
            using (var settingContext = new OcvSettingDbContext())
            {
                var connectStringItem = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "SqlServerConnectString");
                connectStr = connectStringItem?.JsonValue??string.Empty;
            }
            string connectString = connectStr;
            optionsBuilder.UseSqlServer(connectString);
        }
    }
}
