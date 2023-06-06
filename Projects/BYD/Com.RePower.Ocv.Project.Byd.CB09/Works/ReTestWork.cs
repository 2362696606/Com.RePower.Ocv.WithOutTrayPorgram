using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult ReTest()
    {
        if (TestOption.Default.IsDoRetest)
        {
            int retestTimes = 0;
            while (Tray.NgInfos.Any(x => x.IsNg) && retestTimes < TestOption.Default.RetestTime)
            {
                var sendRetestSingalResult = SendRetestSignal();
                if (sendRetestSingalResult.IsFailed)
                    return sendRetestSingalResult;
                LogHelper.UiLog.Info("等待测试请求");
                var waitResult = WaitTestRequest();
                if (waitResult.IsFailed)
                    return waitResult;
                LogHelper.UiLog.Info("等待测试请求成功");
                var sendResult = SendCoordinates();
                if(sendResult.IsFailed)
                    return sendResult;
                LogHelper.UiLog.Info("下发坐标");
                sendResult = SendReadTrayCodeComplete();
                if(sendResult.IsFailed)
                    return sendResult;
                waitResult = WaitTestRequest();
                if (waitResult.IsFailed)
                    return waitResult;
                LogHelper.UiLog.Info("等待测试请求成功");
                LogHelper.UiLog.Info("测试Ng电池");
                var ngBatteries = Tray.NgInfos.Where(x => x.IsNg);
                foreach (var ngBattery in ngBatteries)
                {
                    var testResult = TestOneBattery(ngBattery);
                    if(testResult.IsFailed)
                        return testResult;
                }
                LogHelper.UiLog.Info("测试Ng电池成功");
                LogHelper.UiLog.Info("验证Ng信息");
                var verifyResult = VerifyNg();
                if(verifyResult.IsFailed)
                    return verifyResult;
                LogHelper.UiLog.Info("验证Ng信息成功");
                retestTimes++;
            }
        }
        return OperateResult.CreateSuccessResult();
    }
    protected virtual OperateResult SendRetestSignal()
    {
        LogHelper.UiLog.Info("下发复测信号");
        var writeResult = Plc.Write(PlcCacheSetting["Group2"]["测试标志"].Address, (short)3);
        if (writeResult.IsSuccess)
            LogHelper.UiLog.Info("下发复测信号成功");
        return writeResult;
    }
    protected virtual OperateResult WaitRetestRequest()
    {
        var waitResult = Plc.Wait(PlcCacheSetting["Group1"]["请求测试"].Address, (short)3,
            cancellation: FlowController.CancelToken);
        return waitResult;
    }
}