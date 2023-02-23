using Com.RePower.Ocv.Ui.Byd.CB15.Views;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
        }
        [RelayCommand]
        private void OpenSettingView()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
    }
}
