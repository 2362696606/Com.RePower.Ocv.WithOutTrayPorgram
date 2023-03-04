using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public class SettingViewModel : ObservableObject
    {
        public SettingManager SettingManager => SettingManager.Instance;
    }
}