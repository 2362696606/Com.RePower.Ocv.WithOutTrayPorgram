using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public class TrayViewModel : ObservableObject
    {
        public TrayViewModel(Tray? tray = null)
        {
            this._tray = tray;
        }
        private Tray? _tray;

        public Tray? Tray
        {
            get { return _tray; }
            set { SetProperty(ref _tray, value); }
        }
        public SettingManager SettingManager
        {
            get { return SettingManager.Instance; }
        }
    }
}
