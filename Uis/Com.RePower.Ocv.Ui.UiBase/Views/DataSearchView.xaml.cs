using Com.RePower.Ocv.Model.DataBaseContext;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace Com.RePower.Ocv.Ui.UiBase.Views
{
    /// <summary>
    /// DataSearchView.xaml 的交互逻辑
    /// </summary>
    public partial class DataSearchView : UserControl
    {
        public DataSearchView()
        {
            InitializeComponent();
        }
        public LocalTestResultDbContext DbContext
        {
            get { return (LocalTestResultDbContext)GetValue(DbContextProperty); }
            set { SetValue(DbContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DbContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DbContextProperty =
            DependencyProperty.Register("DbContext", typeof(LocalTestResultDbContext), typeof(DataSearchView), new PropertyMetadata(new LocalTestResultDbContext()));


        private async void DoSearch(object sender, RoutedEventArgs e)
        {
            this.DoSearchButton.IsEnabled = false;
            this.DialogHost.IsOpen = true;
            await Task.Run(() =>
            {
                string trayCode = TrayCodeTextBox.Text;
                string batteryCode = BatteryCodeTextBox.Text;
                var startTime = StartDateTimePicker.SelectedDateTime;
                var endTime = EndDateTimePicker.SelectedDateTime;
                //var startTime = StartDatePicker.SelectedDate;
                DbContext.Batterys.OrderBy(x => x.Id).Where(x=>
                string.IsNullOrEmpty(trayCode)?true:(x.TrayCode == trayCode)
                && string.IsNullOrEmpty(batteryCode)?true:(x.BarCode == batteryCode)
                );
            });
            this.DialogHost.IsOpen = false;
            this.DoSearchButton.IsEnabled = true;
        }

        private void Pagination_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {

        }
    }
}
