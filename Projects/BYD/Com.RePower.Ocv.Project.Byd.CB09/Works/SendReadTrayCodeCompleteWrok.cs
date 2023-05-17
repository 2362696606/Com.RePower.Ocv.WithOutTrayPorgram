using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{

    /// <summary>
    /// 下发"读码、下发坐标完成"信号
    /// </summary>
    /// <returns>下发结果</returns>
    protected virtual OperateResult SendReadTrayCodeComplete()
    {
        var writeResult = Plc.Write(PlcCacheSetting["Group2"]["测试标志"].Address,(short)1);
        return writeResult;
    }
}