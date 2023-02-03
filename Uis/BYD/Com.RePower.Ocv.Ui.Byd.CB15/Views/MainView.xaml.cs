using Com.RePower.Ocv.Ui.Byd.CB15.ViewModels;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Com.RePower.Ocv.Ui.Byd.CB15.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = IocHelper.Default.GetService<MainViewModel>();
            WeakReferenceMessenger.Default.Register<string, string>(this, "MainSnackbar", OnShowMessage);
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
