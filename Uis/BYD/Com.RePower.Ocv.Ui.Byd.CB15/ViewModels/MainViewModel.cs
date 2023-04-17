using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
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

        public bool IsOcv4 
        { 
            get
            {
                if(SettingManager.Instance.CurrentOcvType == Project.Byd.CB15.Enums.OcvTypeEnmu.OCV4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }

        [RelayCommand]
        private void OpenSettingView()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
        [RelayCommand]
        private void OpenChannelNgInfoView()
        {
            var view = new ChannelNgInfosView();
            view.Show();
        }

        [RelayCommand]
        private void OpenDataSearch()
        {
            var dataSearchView = new DataSearchView();
            dataSearchView.Show();
        }
        [RelayCommand]
        private void OpenAcirOption()
        {
            var view = new AcirOptionView();
            view.Show();
        }
    }
}
