using Com.RePower.Ocv.Ui.WuWei.ViewModels;
using Com.RePower.WpfBase;
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
        }
    }
}
