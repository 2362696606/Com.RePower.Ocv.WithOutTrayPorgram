using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 测试电池
    /// </summary>
    /// <returns>测试结果</returns>
    protected virtual OperateResult TestBatteries()
    {
        var closeResult = CloseAllChannel();
        if (closeResult.IsFailed)
            return closeResult;
        foreach (var ngInfo in Tray.NgInfos)
        {
            var testResult = TestOneBattery(ngInfo);
            if (testResult.IsFailed) return testResult;
            DoPauseOrStop();
        }
        if (TestOption.Default.IsTestTemp)
        {
            var testResult = TestTemperature();
            if (testResult.IsFailed) return testResult;
            DoPauseOrStop();
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult TestTemperature()
    {
        var result = TemperatureSensor.ReadTemp();
        if (result.IsFailed) return result;
        double[] tempValues = result.Content ?? throw new ArgumentNullException(nameof(result.Content));
        for (int i = 0; i < Tray.NgInfos.Count(); i++)
        {
            var battery = Tray.NgInfos.First(x => x.Battery.Position == (i + 1)).Battery;
            battery.Temp = tempValues[i];
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult CloseAllChannel()
    {
        foreach (var item in SwitchBoardChannelSetting.Default)
        {
            var closeResult = SwitchBoard.CloseAllChannels(item.BoardIndex);
            if(closeResult.IsFailed)
                return closeResult;
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult TestOneBattery(NgInfo ngInfo)
    {
        LogHelper.UiLog.Info($"测试电池{ngInfo.Battery.Position}");
        var openResult = OpenChannelByBattery(ngInfo.Battery.Position);
        if(openResult.IsFailed)
            return openResult;
        if (TestOption.Default.IsTestVol)
        {
            LogHelper.UiLog.Info("读取电压");
            var readResult = Dmm.ReadDc();
            if (readResult.IsFailed)
                return readResult;
            ngInfo.Battery.VolValue = readResult.Content;
            LogHelper.UiLog.Info("读取电压成功");
        }
        DoPauseOrStop();
        if (TestOption.Default.IsTestRes)
        {
            LogHelper.UiLog.Info("读取内阻");
            var readResult = Ohm.ReadRes();
            if(readResult.IsFailed)
                return readResult;
            if (CalibrationSetting.Default.IsUseCalibration)
            {
                var calibrationItem = CalibrationSetting.Default[ngInfo.Battery.Position];
                if (CalibrationSetting.Default.IsUseAutoCalibration)
                {
                    ngInfo.Battery.Res = readResult.Content + calibrationItem.AutoCalibrationValue;
                }
                else
                {
                    ngInfo.Battery.Res = readResult.Content + calibrationItem.ManualCalibrationValue;
                }
            }
            else
            {
                ngInfo.Battery.Res = readResult.Content;
            }
            LogHelper.UiLog.Info("读取内阻成功");
        }
        var closeResult = OpenChannelByBattery(ngInfo.Battery.Position, SwitchBoardMode.Vol, false);
        if(closeResult.IsFailed)
            return closeResult;
        DoPauseOrStop();
        if (TestOption.Default.IsTestPVol)
        {
            LogHelper.UiLog.Info("读取正极壳体电压");
            openResult = OpenChannelByBattery(ngInfo.Battery.Position, SwitchBoardMode.PVol);
            if (openResult.IsFailed)
                return openResult;
            var readResult = Dmm.ReadDc();
            if(readResult.IsFailed)
                return readResult;
            ngInfo.Battery.PVolValue = readResult.Content;
            closeResult = OpenChannelByBattery(ngInfo.Battery.Position, SwitchBoardMode.PVol, false);
            if (closeResult.IsFailed)
                return closeResult;
            LogHelper.UiLog.Info("读取正极壳体电压成功");
        }
        DoPauseOrStop();
        if (TestOption.Default.IsTestNVol)
        {
            LogHelper.UiLog.Info("读取负极壳体电压");
            openResult = OpenChannelByBattery(ngInfo.Battery.Position, SwitchBoardMode.NVol);
            if (openResult.IsFailed)
                return openResult;
            var readResult = Dmm.ReadDc();
            if (readResult.IsFailed)
                return readResult;
            ngInfo.Battery.NVolValue = readResult.Content;
            closeResult = OpenChannelByBattery(ngInfo.Battery.Position, SwitchBoardMode.NVol, false);
            if (closeResult.IsFailed)
                return closeResult;
            LogHelper.UiLog.Info("读取负极壳体电压成功");
        }

        ngInfo.Battery.IsTested = true;
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult OpenChannelByBattery(int position,SwitchBoardMode mode = SwitchBoardMode.Vol,bool isOpen = true)
    {
        SwitchBoardChannelConfig config =
            SwitchBoardChannelSetting.Default.First(x => x.Correspondence.ContainsKey(position));
        int boardIndex = config.BoardIndex;
        int channel = config.Correspondence[position];
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
        var result = isOpen ? SwitchBoard.OpenChannels(boardIndex, allChannel.ToArray()) : SwitchBoard.CloseChannels(boardIndex, allChannel.ToArray());
        if (result.IsSuccess)
        {
            LogHelper.UiLog.Info($"{option}通道\"{string.Join(',',allChannel)}\"成功");
        }

        return result;
    }
}