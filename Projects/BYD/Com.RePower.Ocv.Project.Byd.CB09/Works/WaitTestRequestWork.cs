using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult WaitTestRequest()
    {
        var waitResult = _plc.Wait(_plcCacheSetting["Group1"]["请求测试"].Address, (short)2,
            cancellation: FlowController.CancelToken);
        return waitResult;
    }
}