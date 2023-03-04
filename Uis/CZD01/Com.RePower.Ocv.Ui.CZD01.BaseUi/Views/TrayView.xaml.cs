using Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels;
using Com.RePower.WpfBase;
using System.Windows.Controls;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.Views
{
    /// <summary>
    /// TrayView.xaml 的交互逻辑
    /// </summary>
    public partial class TrayView : UserControl
    {
        public TrayView()
        {
            InitializeComponent();
            this.DataContext = IocHelper.Default.GetService<TrayViewModel>();
        }
    }
}
