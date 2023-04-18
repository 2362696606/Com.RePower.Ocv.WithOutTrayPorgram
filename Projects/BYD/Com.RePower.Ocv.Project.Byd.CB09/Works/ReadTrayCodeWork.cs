using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult ReadTrayCode()
    {
        var readResult = _plc.ReadString(_plcCacheSetting["Group1"]["上托盘条码信息"].Address,
            (ushort)_plcCacheSetting["Group1"]["上托盘条码信息"].Length);
        if (readResult.IsFailed)
            return readResult;
        if(string.IsNullOrEmpty(readResult.Content))
            return  OperateResult.CreateFailedResult("Plc上传的条码为空");
        _tray.TrayCode = readResult.Content;
        return OperateResult.CreateSuccessResult();
    }
}