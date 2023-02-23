using Com.RePower.Ocv.Ui.CZD01.BaseUi.Views;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public partial class MainWindowViewModel:ObservableObject
    {
        [RelayCommand]
        private void OpenSettingView()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
    }
}
