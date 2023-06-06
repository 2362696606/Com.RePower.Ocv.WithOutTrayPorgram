namespace Com.RePower.Ocv.Project.Byd.CB09.Models;

public class CalibrationItem
{
    /// <summary>
    /// 通道号
    /// </summary>
    public int Channel { get; set; }
    /// <summary>
    /// 自动校准值
    /// </summary>
    public double AutoCalibrationValue { get; set; }
    /// <summary>
    /// 手动校准值
    /// </summary>
    public double ManualCalibrationValue { get; set; }
    /// <summary>
    /// 标准值
    /// </summary>
    public double StandRes { get; set; }
}