using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works;

public partial class MainWork
{
    public OperateResult CleanWarring()
    {
        LogHelper.UiLog.Info("清除报警");
        string address = SettingManager.PlcValueCacheSetting?["异常ng报警"]?.Address ?? string.Empty;
        if (string.IsNullOrEmpty(address))
            return OperateResult.CreateFailedResult("无法获取\"异常ng报警\"地址");
        var writeResult = DevicesController.Plc?.Write(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
        if (writeResult.IsFailed)
            return writeResult;
        return OperateResult.CreateSuccessResult();
    }
    public OperateResult SendWarring(int warringFlag)
    {
        LogHelper.UiLog.Info($"写入报警,值:{warringFlag}");
        string address = SettingManager.PlcValueCacheSetting?["异常ng报警"]?.Address ?? string.Empty;
        if (string.IsNullOrEmpty(address))
            return OperateResult.CreateFailedResult("无法获取\"异常ng报警\"地址");
        var writeResult = DevicesController.Plc?.Write(address, (short)warringFlag) ?? OperateResult.CreateFailedResult("Plc实例为null");
        if (writeResult.IsFailed)
            return writeResult;
        return OperateResult.CreateSuccessResult();
    }
}