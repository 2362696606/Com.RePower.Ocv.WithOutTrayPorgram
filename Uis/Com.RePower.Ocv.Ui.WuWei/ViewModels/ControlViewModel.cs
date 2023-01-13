using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.WuWei.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.WuWei.ViewModels
{
    public partial class ControlViewModel:ObservableObject
    {
        public ControlViewModel(IProjectMainWork projectMainWork)
        {
            this._work = projectMainWork as MainWorkFixed;
        }

        private MainWorkFixed? _work;

        public MainWorkFixed? Work
        {
            get 
            {
                if(this._work == null)
                {
                    WeakReferenceMessenger.Default.Send("未能成功获取流程实现", "MainSnackbar");
                }
                return _work;
            }
        }

        [RelayCommand]
        private void DoStart()
        {
            if (Work?.WorkStatus == 0||Work?.WorkStatus == 2)
                Work.StartWorkAsync();
            else
                Work?.PauseWorkAsync();
        }
        [RelayCommand]
        private void DoStop()
        {
            Work?.StopWorkAsync();
        }
        //[RelayCommand]
        //private void OutPutTray()
        //{
        //    Work?.SendOutPutTray();
        //}
        [RelayCommand(CanExecute = nameof(CanReSetPlc))]
        private async void ReSetPlc()
        {
            if(Work is { })
            {
                var result = await Work.ReSetPlcAsync();
                if(result.IsFailed)
                {
                    WeakReferenceMessenger.Default.Send($"发送复位信号失败:{result.Message}", "MainSnackbar");
                }
            }
        }

        private bool CanReSetPlc()
        {
            if(Work?.WorkStatus == 1)
            {
                return false;
            }
            return true;
        }
    }
}
