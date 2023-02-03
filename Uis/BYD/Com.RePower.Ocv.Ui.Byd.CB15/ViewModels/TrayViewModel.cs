using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class TrayViewModel:ObservableObject
    {
        [ObservableProperty]
        private Tray _tray;
        [ObservableProperty]
        private SettingManager _settingManager;

        public TrayViewModel(Tray tray,SettingManager settingManager)
        {
            this._tray = tray;
            this._settingManager = settingManager;
        }

    }
}
