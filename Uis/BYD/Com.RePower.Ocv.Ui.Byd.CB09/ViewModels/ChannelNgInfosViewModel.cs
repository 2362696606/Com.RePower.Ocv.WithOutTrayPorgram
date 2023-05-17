using System;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels;

public class ChannelNgInfosViewModel:ObservableObject
{
    public ChannelNgInfosViewModel()
    {
        CleanChannelNgTimesCommand = new RelayCommand<ChannelNgInfo>(CleanChannelNgTimes);
    }
    public ChannelsNgInfoCache Cache => ChannelsNgInfoCache.Default;

    public RelayCommand<ChannelNgInfo> CleanChannelNgTimesCommand { get; }
    private void CleanChannelNgTimes(ChannelNgInfo? channelNgInfo)
    {
        if (channelNgInfo == null)
        {
            throw new ArgumentNullException();
        }
        channelNgInfo.NgTimes = 0;
        Cache.SaveChanged();
    }
}