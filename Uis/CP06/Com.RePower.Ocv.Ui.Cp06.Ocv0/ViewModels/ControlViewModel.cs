using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class ControlViewModel:ObservableObject
    {
        public ControlViewModel(IProjectMainWork work)
        {
            this._work = work as MainWork;
        }
        private MainWork? _work;

        public MainWork? Work
        {
            get
            {
                if (this._work == null)
                {
                    WeakReferenceMessenger.Default.Send("未能成功获取流程实现", "MainSnackbar");
                }
                return _work;
            }
        }
        [RelayCommand]
        private void DoStart()
        {
            if (Work?.WorkStatus == 0 || Work?.WorkStatus == 2)
                Work.StartWorkAsync();
            else
                Work?.PauseWorkAsync();
        }
        [RelayCommand]
        private void DoStop()
        {
            Work?.StopWorkAsync();
        }
    }
}
