using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class TrayViewModel:ObservableObject
    {
        [ObservableProperty]
        private Tray _tray;

        public SettingManager SettingManager => SettingManager.Instance;


        public TrayViewModel(Tray tray)
        {
            this._tray = tray;
        }

    }
}
