using Com.RePower.Ocv.Model.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public class SettingManager
    {
        private SettingManager()
        {
            using (var context = new OcvSettingDbContext())
            {
            }
        }
    }
}
