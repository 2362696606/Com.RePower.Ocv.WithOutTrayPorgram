using Com.RePower.Ocv.Ui.WuWei.ViewModels;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Windows;

namespace Com.RePower.Ocv.Ui.WuWei.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            this.DataContext = IocHelper.Default.GetService<MainViewModel>();
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<string,string>(this,"MainSnackbar", OnShowMessage);
        }

        private void OnShowMessage(object recipient, string message)
        {
            if (MainSnackbar.MessageQueue is { } messageQueue)
            {
                messageQueue.Enqueue(message, "Close", () =>
                {
                    Debug.WriteLine("Close clicked");
                });
            }
        }
    }
}
