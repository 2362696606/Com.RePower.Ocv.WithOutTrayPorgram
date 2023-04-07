using Com.RePower.Ocv.Model.DataBaseContext;

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