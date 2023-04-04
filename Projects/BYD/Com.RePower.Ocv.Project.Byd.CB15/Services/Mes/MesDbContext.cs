using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes
{
    public class MesDbContext:DbContext
    {
        public MesDbContext(DbContextOptions<MesDbContext> option):base(option)
        {

        }
        public MesDbContext():base()
        {

        }

        public DbSet<Ocv1InfoDto> Ocv1BatData { get; set; }
        public DbSet<Ocv2InfoDto> Ocv2BatData { get; set; }
        public DbSet<Ocv3InfoDto> Ocv3BatData { get; set; }
        public DbSet<Ocv4InfoDto> Ocv4BatData { get; set; }
    }
}