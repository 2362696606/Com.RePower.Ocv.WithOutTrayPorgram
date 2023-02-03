using Com.RePower.Ocv.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.DataBaseContext
{
    public class LocalTestResultDbContext:DbContext
    {
        public DbSet<BatteryDto> Batterys { get; set; }
        public DbSet<NgInfoDto> NgInfos { get; set; }
        public DbSet<TrayDto> Trays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectString = @"data source=.\LocalTestResult.dbs";
            optionsBuilder.UseSqlite(connectString);
            //optionsBuilder.UseLazyLoadingProxies();
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
