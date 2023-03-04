using Com.RePower.Ocv.Project.YiWei.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels.Windows
{
    public class SettingViewModel:ObservableObject
    {
        public SettingManager SettingManager => Project.ProjectBase.Controllers.SettingManager<SettingManager>.Instance;
    }
}
