using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 等待请求读取条码
    /// </summary>
    /// <returns>等待结果</returns>
    protected virtual OperateResult WaitTrayCodeRequest()
    {
        var waitResult = Plc.Wait(PlcCacheSetting["Group1"]["请求测试"].Address, (short)1,
            cancellation: FlowController.CancelToken);
        return waitResult;
    }
}