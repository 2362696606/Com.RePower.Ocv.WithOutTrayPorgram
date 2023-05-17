using System;
using System.Collections.Generic;
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
using Com.RePower.Ocv.Ui.Byd.CB09.ViewModels;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Ui.Byd.CB09.Views
{
    /// <summary>
    /// ChannelNgInfosView.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelNgInfosView : UserControl
    {
        public ChannelNgInfosView()
        {
            InitializeComponent();
            this.DataContext = IocHelper.Default.GetService<ChannelNgInfosViewModel>();
        }
    }
}
