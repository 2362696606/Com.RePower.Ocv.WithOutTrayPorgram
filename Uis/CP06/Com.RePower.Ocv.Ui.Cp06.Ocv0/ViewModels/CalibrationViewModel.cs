﻿using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers.Works;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class CalibrationViewModel:ObservableObject
    {
        public CalibrationViewModel(IProjectMainWork work)
        {
            Work = work;
        }

        public SettingManager SettingManager => SettingManager.Instance;
        [RelayCommand]
        private void DoCalibration()
        {
            if(Work is MainWork tempWork)
            {

            }
        }
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

        public IProjectMainWork Work { get; }
    }
}
