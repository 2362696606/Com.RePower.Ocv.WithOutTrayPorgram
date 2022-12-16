using Com.RePower.Ocv.Ui.WuWei.ViewModels;
using Com.RePower.WpfBase;
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

namespace Com.RePower.Ocv.Ui.WuWei.Views
{
    /// <summary>
    /// MonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorView : UserControl
    {
        public MonitorView()
        {
            this.DataContext = IocHelper.Default.GetService<MonitorViewModel>();
            InitializeComponent();
        }
    }
}
