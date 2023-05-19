using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 验证Ng
    /// </summary>
    /// <returns>验证Ng</returns>
    protected OperateResult VerifyNg()
    {
        foreach (var ngInfo in Tray.NgInfos)
        {
            LogHelper.UiLog.Info($"验证电池{ngInfo.Battery.Position}Ng信息");
            if (TestOption.Default.IsTestVol)
            {
                if ((ngInfo.Battery.VolValue??0) > BatteryStandard.Default.MaxVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.电压过高);
                }
                if ((ngInfo.Battery.VolValue ?? 0) < BatteryStandard.Default.MinVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.电压过低);
                }
            }
            if (TestOption.Default.IsTestRes)
            {
                if ((ngInfo.Battery.Res ?? 0) > BatteryStandard.Default.MaxRes)
                {
                    ngInfo.AddNgType(NgTypeEnum.内阻过高);
                }
                if ((ngInfo.Battery.Res ?? 0) < BatteryStandard.Default.MinRes)
                {
                    ngInfo.AddNgType(NgTypeEnum.内阻过低);
                }
            }
            if (TestOption.Default.IsTestPVol)
            {
                if ((ngInfo.Battery.PVolValue ?? 0) > BatteryStandard.Default.MaxPVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.正极壳体电压过高);
                }
                if ((ngInfo.Battery.PVolValue ?? 0) < BatteryStandard.Default.MinPVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.正极壳体电压过低);
                }
            }
            if (TestOption.Default.IsTestNVol)
            {
                if ((ngInfo.Battery.NVolValue ?? 0) > BatteryStandard.Default.MaxNVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.负极壳体电压过高);
                }
                if ((ngInfo.Battery.NVolValue ?? 0) < BatteryStandard.Default.MinNVol)
                {
                    ngInfo.AddNgType(NgTypeEnum.负极壳体电压过低);
                }
            }
            if (TestOption.Default.IsTestTemp)
            {
                if ((ngInfo.Battery.Temp ?? 0) > BatteryStandard.Default.MaxTemp)
                {
                    ngInfo.AddNgType(NgTypeEnum.温度过高);
                }
                if ((ngInfo.Battery.Temp ?? 0) < BatteryStandard.Default.MinTemp)
                {
                    ngInfo.AddNgType(NgTypeEnum.温度过低);
                }
            }
            if (ngInfo.IsNg)
            {
                var switchBoard = SwitchBoardChannelSetting.Default
                    .First(x => x.Correspondence.ContainsKey(ngInfo.Battery.Position));
                var boardIndex = switchBoard.BoardIndex;
                var channel = switchBoard.Correspondence[ngInfo.Battery.Position];
                var channelNgInfo = ChannelsNgInfoCache.Default.ChannelsGroups.First(x => x.BoardIndex == boardIndex).Nginfos
                    .First(x => x.Channel == channel);
                channelNgInfo.NgTimes += 1;
                ChannelsNgInfoCache.Default.SaveChanged();
            }
            LogHelper.UiLog.Info($"验证电池{ngInfo.Battery.Position}Ng信息成功");
        }
        return OperateResult.CreateSuccessResult();
    }
}