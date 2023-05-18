using Com.RePower.WpfBase;
using static NPOI.HSSF.Util.HSSFColor;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Text.RegularExpressions;

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
        var trayCode = Regex.Match(readResult.Content ?? string.Empty, @"[0-9\.a-zA-Z_-]+").Value;
        if (string.IsNullOrEmpty(trayCode))
            return OperateResult.CreateFailedResult("Plc上传的条码为空");
        Tray.TrayCode = trayCode;
        return OperateResult.CreateSuccessResult();
    }
}