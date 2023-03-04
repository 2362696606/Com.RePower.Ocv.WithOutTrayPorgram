using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Entities;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public class ChannelNgInfosViewModel:ObservableObject
    {
        public ChannelNgInfosViewModel()
        {
            CleanChannelNgTimesCommand = new AsyncRelayCommand<ChannelNgInfo>(CleanChannelNgTimes);
            MessageQueue = new SnackbarMessageQueue();
        }
        public List<ChannelNgInfo>? ChannelNgInfos => SettingManager.Instance.ChannelNgInfos;

        public SnackbarMessageQueue MessageQueue { get; set; }

        public IAsyncRelayCommand<ChannelNgInfo> CleanChannelNgTimesCommand { get; set; }

        private async Task CleanChannelNgTimes(ChannelNgInfo? channelNgInfo)
        {
            await Task.Factory.StartNew(() =>
            {
                var result = channelNgInfo?.CleanTimes() ?? OperateResult.CreateFailedResult("当前通道实例为null");
                if(result.IsFailed)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageQueue.Enqueue(result?.Message ?? $"通道{channelNgInfo?.Channel}清零失败:未知原因");
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageQueue.Enqueue($"通道{channelNgInfo?.Channel}清零成功");
                    });
                }
            });
        }
    }
}
