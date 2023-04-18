using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult SendReadTrayCodeComplete()
    {
        var writeResult = _plc.Write(_plcCacheSetting["Group2"]["测试标志"].Address,(short)1);
        return writeResult;
    }
}