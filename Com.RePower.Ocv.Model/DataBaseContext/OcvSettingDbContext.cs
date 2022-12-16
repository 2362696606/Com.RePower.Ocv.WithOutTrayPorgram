using Com.RePower.Ocv.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.DataBaseContext
{
    public class OcvSettingDbContext:DbContext
    {
        public DbSet<OcvSettingItemDto> SettingItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectString = @"data source=D:\Code\Com.RePower.Ocv.WithOutTrayPorgram\Projects\Com.RePower.Ocv.Project.WuWei\OcvSetting.dbs";
            //string connectString = @"data source=.\OcvSetting.dbs";
            optionsBuilder.UseSqlite(connectString);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
