using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Settings
{
    public class MesSetting
    {
        public string DbConnectString { get; set; } = "Data Source=172.22.65.199;Initial Catalog=CB15_OCV;User Id=repower;Password=Admin@123;TrustServerCertificate=true;";
        public string FtpService { get; set; } = "";
        public string FtpUserName { get; set; } = "";
        public string FtpUserPassword { get; set; } = "";
    }
}
