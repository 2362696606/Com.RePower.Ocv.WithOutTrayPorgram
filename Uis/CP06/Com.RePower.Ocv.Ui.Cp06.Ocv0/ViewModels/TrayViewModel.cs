﻿using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class TrayViewModel:ObservableObject
    {
        [ObservableProperty]
        private Tray _tray;
        public SettingManager SettingManager => SettingManager.Instance;

        public TrayViewModel(Tray tray)
        {
            this._tray = tray;
        }

    }
}
