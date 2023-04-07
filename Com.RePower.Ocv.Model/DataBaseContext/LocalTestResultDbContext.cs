using Com.RePower.Ocv.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Com.RePower.Ocv.Model.DataBaseContext
{
    public class LocalTestResultDbContext : DbContext
    {
        //public LocalTestResultDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        //public LocalTestResultDbContext(DbContextOptions<LocalTestResultDbContext> dbContextOptions):base(dbContextOptions) { }
        //public LocalTestResultDbContext():base()
        //{
        //}
        public DbSet<BatteryDto> Batterys { get; set; }

        public DbSet<NgInfoDto> NgInfos { get; set; }
        public DbSet<TrayDto> Trays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectString = @"data source=.\LocalTestResult.dbs";
            optionsBuilder.UseSqlite(connectString);
        }
    }
}