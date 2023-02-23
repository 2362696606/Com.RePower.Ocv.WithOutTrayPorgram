using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Ui.WuWei.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.WuWei.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        [RelayCommand]
        private void OpenSetting()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
    }
}
