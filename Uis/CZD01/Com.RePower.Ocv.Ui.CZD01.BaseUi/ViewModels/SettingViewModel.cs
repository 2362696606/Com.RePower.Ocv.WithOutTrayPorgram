using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public class SettingViewModel:ObservableObject
    {
        public SettingManager SettingManager => SettingManager.Instance;
    }
}
