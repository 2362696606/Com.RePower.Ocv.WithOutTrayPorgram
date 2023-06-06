using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;
using Com.RePower.Ocv.Ui.CZD01.BaseUi.UiHelper;
using Com.RePower.Ocv.Ui.CZD01.BaseUi.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            WeakReferenceMessenger.Default.Register<DoMethodMessage,string>(this, "DoMainViewMethod", MessageHelper.DoMethod);
        }

        public void DoWarring(Dictionary<string, object> parameters)
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                var viewModel = new WarringViewModel();
                var view = new WarringView();
                if (parameters.TryGetValue("warringInfo", out object? obj))
                {
                    viewModel.WarringInfo = Convert.ToString(obj) ?? throw new ArgumentNullException();
                }

                view.DataContext = viewModel;
                await DialogHost.Show(view, "MainDialog");
                if (parameters.TryGetValue("flag", out object? flag))
                {
                    if (flag is ManualResetEvent resetEvent)
                    {
                        resetEvent.Set();
                    }
                }
            });
        }

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
