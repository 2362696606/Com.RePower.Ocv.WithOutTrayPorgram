﻿using Com.RePower.Ocv.Ui.YiWei.ViewModels.Windows;
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
using System.Windows.Shapes;

namespace Com.RePower.Ocv.Ui.YiWei.Views.Windows
{
    /// <summary>
    /// DataSearchView.xaml 的交互逻辑
    /// </summary>
    public partial class DataSearchView : Window
    {
        public DataSearchView()
        {
            InitializeComponent();
            DataContext = IocHelper.Default.GetService<DataSearchViewModel>();
        }
    }
}