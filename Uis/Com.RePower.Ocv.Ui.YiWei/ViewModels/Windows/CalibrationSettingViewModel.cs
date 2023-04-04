using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.Ocv.Project.YiWei.Controllers;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels.Windows
{
    public partial class CalibrationSettingViewModel:ObservableObject
    {
        public SettingManager SettingManager => SettingManager<SettingManager>.Instance;
        [RelayCommand]
        private async void DoSaveChanged()
        {
            if (SettingManager.CurrentCalibrationSetting is ISettingSaveChanged tempSetting)
            {
                var result = await tempSetting.SaveChangedAsync();
                if (result.IsFailed)
                    MessageQueue.Enqueue($"保存失败:{result.Message}");
                else
                    MessageQueue.Enqueue($"保存成功");
            }
            else
                MessageQueue.Enqueue($"配置非继承自\"ISettingSaveChanged\"");
        }
        private SnackbarMessageQueue _messageQueue = new SnackbarMessageQueue();
        /// <summary>
        /// 消息队列
        /// </summary>
        public SnackbarMessageQueue MessageQueue
        {
            get { return _messageQueue; }
            set { SetProperty(ref _messageQueue, value); }
        }

    }
}
