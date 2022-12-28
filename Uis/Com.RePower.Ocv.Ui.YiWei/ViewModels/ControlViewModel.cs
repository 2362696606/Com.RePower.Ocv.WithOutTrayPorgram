using Com.RePower.Ocv.Project;
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
        private IProjectMainWork work;
        public ControlViewModel(IProjectMainWork projectMainWork)
        {
            this.work = projectMainWork;
        }
        [RelayCommand]
        private void DoStart()
        {
            work.StartWorkAsync();
        }
        [RelayCommand]
        private void DoStop()
        {
            work.StopWorkAsync();
        }
        [RelayCommand]
        private void DoPause() 
        {
            work.PauseWorkAsync();
        }
    }
}
