using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class CraftViewModel:ObservableObject
    {
        [ObservableProperty]
        private SettingManager _settingManager;

        public CraftViewModel(SettingManager settingManager)
        {
            this._settingManager = settingManager;
        }
    }
}
