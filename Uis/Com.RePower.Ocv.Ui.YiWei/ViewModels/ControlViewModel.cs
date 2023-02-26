﻿using Com.RePower.Ocv.Project;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public partial class ControlViewModel:ObservableObject
    {
        public IProjectMainWork Work { get; }
        public ControlViewModel(IProjectMainWork projectMainWork)
        {
            this.Work = projectMainWork;
        }
        [RelayCommand]
        private void DoStart()
        {
            Work.StartWorkAsync();
        }
        [RelayCommand]
        private void DoStop()
        {
            Work.StopWorkAsync();
        }
        [RelayCommand]
        private void DoPause() 
        {
            Work.PauseWorkAsync();
        }
    }
}
