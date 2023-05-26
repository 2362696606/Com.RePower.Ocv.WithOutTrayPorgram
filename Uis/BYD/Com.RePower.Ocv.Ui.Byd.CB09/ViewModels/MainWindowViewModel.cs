using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Ui.Byd.CB09.Views;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    public class MainWindowViewModel:ObservableObject
    {
        public MainWindowViewModel()
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            this.OpenSettingsCommand = new RelayCommand(OpenSettings);
            this.OpenSerialPortHelperCommand = new RelayCommand(OpenSerialPortHelper);
            OpenDataSearchCommand = new RelayCommand(OpenDataSearch);
        }

        private void OpenSettings()
        {
            SettingsView view = new SettingsView
            {
                DataContext = new SettingsViewModel()
            };
            DialogHost.Show(view, "MainDialog");
        }
        private void OpenSerialPortHelper()
        {
            SerialPortHelperView view = new SerialPortHelperView();
            DialogHost.Show(view, "MainDialog");
        }
        private void OpenDataSearch()
        {
            DataSearchView view = new DataSearchView();
            //DialogHost.Show(view, "MainDialog");
            view.ShowDialog();
        }

        public string AppVersion { get; }
        public RelayCommand OpenSettingsCommand { get; set; }
        public RelayCommand OpenSerialPortHelperCommand { get; set; }
        public RelayCommand OpenDataSearchCommand { get; set; }
    }
}
