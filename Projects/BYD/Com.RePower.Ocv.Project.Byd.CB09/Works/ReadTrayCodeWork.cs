using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 读取托盘条码
    /// </summary>
    /// <returns>读取结果</returns>
    protected virtual OperateResult ReadTrayCode()
    {
        var readResult = Plc.ReadString(PlcCacheSetting["Group1"]["上托盘条码信息"].Address,
            (ushort)PlcCacheSetting["Group1"]["上托盘条码信息"].Length);
        if (readResult.IsFailed)
            return readResult;
        if(string.IsNullOrEmpty(readResult.Content))
            return  OperateResult.CreateFailedResult("Plc上传的条码为空");
        Tray.TrayCode = readResult.Content;
        return OperateResult.CreateSuccessResult();
    }
}