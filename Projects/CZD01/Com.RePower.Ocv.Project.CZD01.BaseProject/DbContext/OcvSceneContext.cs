using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.DbContext
{
    public class OcvSceneContext : LocalTestResultDbContext
    {
        //public OcvSceneContext(DbContextOptions<OcvSceneContext> dbContextOptions) : base(dbContextOptions) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string? connectStr = SettingManager.Instance.SceneConnectString;
            if(!string.IsNullOrEmpty(connectStr))
            {
                optionsBuilder.UseSqlServer(connectStr);
            }
        }
    }
}
