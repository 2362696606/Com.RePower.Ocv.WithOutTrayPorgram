﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model
{
    public partial class Tray:ObservableObject
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        [ObservableProperty]
        private string _trayCode = string.Empty;
        /// <summary>
        /// Ng结果
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<NgInfo> _ngInfos = new ObservableCollection<NgInfo>();
    }
}
