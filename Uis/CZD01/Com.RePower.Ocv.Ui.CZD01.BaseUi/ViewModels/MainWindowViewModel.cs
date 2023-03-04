using Com.RePower.Ocv.Ui.CZD01.BaseUi.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [RelayCommand]
        private void OpenSettingView()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
        [RelayCommand]
        private void OpenDataSearchView()
        {
            var view = new DataSearchView();
            view.Show();
        }
        [RelayCommand]
        private void OpenCalibrationView()
        {
            var view = new CalibrationView();
            view.Show();
        }
    }
}
