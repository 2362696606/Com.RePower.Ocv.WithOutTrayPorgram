using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Ui.Cp06.Ocv0.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
        }

        /// <summary>
        /// 是否可以校准
        /// </summary>
        public bool CanCalibration
        {
            get 
            {
                if (SettingManager.Instance.CurrentOcvType == Project.Cp06.Ocv0.Enums.OcvTypeEnmu.OCV3)
                    return false;
                else return true;
            }
        }

        [RelayCommand]
        private void DoCalibration()
        {
            var calibrationView = new CalibrationView();
            calibrationView.Show();
        }
        [RelayCommand]
        private void OpenSetting()
        {
            var settingView = new SettingView();
            settingView.Show();
        }
        [RelayCommand]
        private void OpenDataSearch()
        {
            var dataSearchView = new DataSearchView();
            dataSearchView.Show();
        }
        
    }
}
