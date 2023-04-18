using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected OperateResult VerifyNg()
    {
        foreach (var ngInfo in _tray.NgInfos)
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
                if ((ngInfo.Battery.Temp ?? 0) > BatteryStandard.Default.MinTemp)
                {
                    ngInfo.AddNgType(NgTypeEnum.温度过低);
                }
            }
            LogHelper.UiLog.Info($"验证电池{ngInfo.Battery.Position}Ng信息成功");
        }
        return OperateResult.CreateSuccessResult();
    }
}