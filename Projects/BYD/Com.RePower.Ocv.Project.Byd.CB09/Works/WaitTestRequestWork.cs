using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 等待测试请求
    /// </summary>
    /// <returns>等待结果</returns>
    protected virtual OperateResult WaitTestRequest()
    {
        var waitResult = Plc.Wait(PlcCacheSetting["Group1"]["请求测试"].Address, (short)2,
            cancellation: FlowController.CancelToken);
        return waitResult;
    }
}