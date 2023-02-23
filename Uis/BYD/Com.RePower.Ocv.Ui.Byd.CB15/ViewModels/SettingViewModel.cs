using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public class SettingViewModel:ObservableObject
    {
        public SettingManager SettingManager => SettingManager.Instance;
    }
}
