using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult TestBatteries()
    {
        var closeResult = CloseAllChannel();
        if (closeResult.IsFailed)
            return closeResult;
        foreach (var ngInfo in _tray.NgInfos)
        {
            TestOneBattery(ngInfo);
            DoPauseOrStop();
        }
        if (TestOption.Default.IsTestTemp)
        {
            //ToDo:测试温度
            DoPauseOrStop();
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult CloseAllChannel()
    {
        foreach (var item in SwitchBoardChannelSetting.Default)
        {
            var closeResult = _switchBoard.CloseAllChannels(item.BoardIndex);
            if(closeResult.IsFailed)
                return closeResult;
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult TestOneBattery(NgInfo ngInfo)
    {
        LogHelper.UiLog.Info($"测试电池{ngInfo.Battery.Position}");
        var openResult = OpenChannelByBattery(ngInfo);
        if(openResult.IsFailed)
            return openResult;
        if (TestOption.Default.IsTestVol)
        {
            LogHelper.UiLog.Info("读取电压");
            var readResult = _dmm.ReadDc();
            if (readResult.IsFailed)
                return readResult;
            ngInfo.Battery.VolValue = readResult.Content;
            LogHelper.UiLog.Info("读取电压成功");
        }
        DoPauseOrStop();
        if (TestOption.Default.IsTestRes)
        {
            LogHelper.UiLog.Info("读取内阻");
            var readResult = _ohm.ReadRes();
            if(readResult.IsFailed)
                return readResult;
            ngInfo.Battery.Res = readResult.Content;
            LogHelper.UiLog.Info("读取内阻成功");
        }
        var closeResult = OpenChannelByBattery(ngInfo, SwitchBoardMode.Vol, false);
        if(closeResult.IsFailed)
            return closeResult;
        DoPauseOrStop();
        if (TestOption.Default.IsTestPVol)
        {
            LogHelper.UiLog.Info("读取正极壳体电压");
            openResult = OpenChannelByBattery(ngInfo, SwitchBoardMode.PVol);
            if (openResult.IsFailed)
                return openResult;
            var readResult = _dmm.ReadDc();
            if(readResult.IsFailed)
                return readResult;
            ngInfo.Battery.PVolValue = readResult.Content;
            closeResult = OpenChannelByBattery(ngInfo, SwitchBoardMode.PVol, false);
            if (closeResult.IsFailed)
                return closeResult;
            LogHelper.UiLog.Info("读取正极壳体电压成功");
        }
        DoPauseOrStop();
        if (TestOption.Default.IsTestNVol)
        {
            LogHelper.UiLog.Info("读取负极壳体电压");
            openResult = OpenChannelByBattery(ngInfo, SwitchBoardMode.NVol);
            if (openResult.IsFailed)
                return openResult;
            var readResult = _dmm.ReadDc();
            if (readResult.IsFailed)
                return readResult;
            ngInfo.Battery.NVolValue = readResult.Content;
            closeResult = OpenChannelByBattery(ngInfo, SwitchBoardMode.NVol, false);
            if (closeResult.IsFailed)
                return closeResult;
            LogHelper.UiLog.Info("读取负极壳体电压成功");
        }

        ngInfo.Battery.IsTested = true;
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult OpenChannelByBattery(NgInfo ngInfo,SwitchBoardMode mode = SwitchBoardMode.Vol,bool isOpen = true)
    {
        SwitchBoardChannelConfig config =
            SwitchBoardChannelSetting.Default.First(x => x.Correspondence.ContainsKey(ngInfo.Battery.Position));
        int boardIndex = config.BoardIndex;
        int channel = config.Correspondence[ngInfo.Battery.Position];
        List<int> attachedChannel = new List<int>();
        if (config.IsNeedAttached)
        {
            switch (mode)
            {
                case SwitchBoardMode.Vol:
                {
                    attachedChannel = new List<int>(config.VolAttachedChannels);
                    break;
                }
                case SwitchBoardMode.NVol:
                {
                    attachedChannel = new List<int>(config.NVolAttachedChannels);
                    break;
                }
                case SwitchBoardMode.PVol:
                {
                    attachedChannel = new List<int>(config.PVolAttachedChannels);
                    break;
                }
            }
        }
        List<int> allChannel = new List<int>{channel};
        allChannel.AddRange(attachedChannel);
        string option = isOpen ? "打开" : "关闭";
        LogHelper.UiLog.Info($"{option}通道\"{string.Join(',', allChannel)}\"");
        var result = isOpen ? _switchBoard.OpenChannels(boardIndex, allChannel.ToArray()) : _switchBoard.CloseChannels(boardIndex, allChannel.ToArray());
        if (result.IsSuccess)
        {
            LogHelper.UiLog.Info($"{option}通道\"{string.Join(',',allChannel)}\"成功");
        }

        return result;
    }
}