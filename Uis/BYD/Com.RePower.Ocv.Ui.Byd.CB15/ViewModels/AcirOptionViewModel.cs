using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class AcirOptionViewModel:ObservableObject
    {
        public AcirOption? AcirOption => SettingManager.Instance.AcirOption;
        [RelayCommand]
        private Task DoSave()
        {
            return Task.Factory.StartNew(() =>
            {
                var result = AcirOption?.SaveChanged() ?? OperateResult.CreateFailedResult("当前AcirOption为null");
                if (result.IsFailed)
                    MessageQueue.Enqueue($"保存失败:{result.Message}");
                else
                    MessageQueue.Enqueue("保存成功");
            });
        }
        private SnackbarMessageQueue _messageQueue = new SnackbarMessageQueue();

        public SnackbarMessageQueue MessageQueue
        {
            get { return _messageQueue; }
            set { SetProperty(ref _messageQueue, value); }
        }

    }
}
