using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult TestComplete()
    {
        var sendResult = SendTestComplete();
        if (sendResult.IsFailed) return sendResult;
        sendResult = SendOutPut();
        if(sendResult.IsFailed) return sendResult;
        return OperateResult.CreateSuccessResult();
    }

    protected virtual OperateResult SendTestComplete()
    {
        LogHelper.UiLog.Info("下发测试完成");
        var writeResult = Plc.Write(PlcCacheSetting["Group2"]["测试标志"].Address, (short)2);
        if (writeResult.IsSuccess)
            LogHelper.UiLog.Info("下发测试完成信号成功");
        return writeResult;
    }

    protected virtual OperateResult SendOutPut()
    {
        LogHelper.UiLog.Info("下发出库");
        var writeResult = Plc.Write(PlcCacheSetting["Group2"]["出库"].Address, (short)1);
        if (writeResult.IsSuccess)
            LogHelper.UiLog.Info("下发出库信号成功");
        return writeResult;
    }
}