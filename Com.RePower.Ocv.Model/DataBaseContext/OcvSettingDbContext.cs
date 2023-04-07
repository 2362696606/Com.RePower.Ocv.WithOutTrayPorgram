using Com.RePower.Ocv.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Com.RePower.Ocv.Model.DataBaseContext
{
    public class OcvSettingDbContext : DbContext
    {
        /// <summary>
        /// 配置表
        /// </summary>
        public DbSet<OcvSettingItemDto> SettingItems { get; set; }

        /// <summary>
        /// 缓存表
        /// </summary>
        public DbSet<OcvCacheValue> CacheValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectString = @"data source=.\OcvSetting.dbs";
            optionsBuilder.UseSqlite(connectString);
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}