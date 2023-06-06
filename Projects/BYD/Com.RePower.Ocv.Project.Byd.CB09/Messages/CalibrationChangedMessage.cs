using Com.RePower.Ocv.Project.Byd.CB09.Models;

namespace Com.RePower.Ocv.Project.Byd.CB09.Messages;

public class CalibrationChangedMessage
{
    /// <summary>
    /// 校准项
    /// </summary>
    public CalibrationItem CalibrationItem { get; set; } = new();
    /// <summary>
    /// 内阻读取值
    /// </summary>
    public double ReadResValue { get; set; }
    /// <summary>
    /// 偏差值
    /// </summary>
    public double SubValue { get; set; }
}