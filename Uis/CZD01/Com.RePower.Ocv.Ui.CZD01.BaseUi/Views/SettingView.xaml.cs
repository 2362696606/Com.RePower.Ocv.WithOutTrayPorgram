using Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels;
using Com.RePower.WpfBase;
using System.Windows;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.Views
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();
            this.DataContext = IocHelper.Default.GetService<SettingViewModel>();
        }
    }
}
