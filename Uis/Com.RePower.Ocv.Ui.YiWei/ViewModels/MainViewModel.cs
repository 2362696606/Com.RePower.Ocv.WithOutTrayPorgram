using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Ui.UiBase.ViewModels;
using Com.RePower.Ocv.Ui.YiWei.Views;
using Com.RePower.Ocv.Ui.YiWei.Views.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public partial class MainViewModel:ObservableObject
    {
        public MainViewModel()
        {
        }
        [RelayCommand]
        private void SettingManager()
        {
            var view = new SettingView();
            view.Show();
        }
        [RelayCommand]
        private void OpenDataSearchView()
        {
            var view = new DataSearchView();
            view.Show();
        }
        [RelayCommand]
        private void OpenCalibrationSetting()
        {
            var view = new CalibrationSettingView();
            view.Show();
        }
    }
}
