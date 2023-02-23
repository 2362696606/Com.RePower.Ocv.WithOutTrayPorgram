using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class CraftViewModel:ObservableObject
    {
        public SettingManager SettingManager => SettingManager.Instance;

        public CraftViewModel()
        {
        }
    }
}
