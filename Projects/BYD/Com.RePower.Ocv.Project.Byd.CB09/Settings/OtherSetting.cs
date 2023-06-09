using System.ComponentModel;
using System.Configuration;
using NPOI.SS.Formula.Functions;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class OtherSetting:ApplicationSettingsBase
{
    public static OtherSetting Default { get; } = ((OtherSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new OtherSetting())));
    /// <summary>
    /// 左压合X轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("左压合X轴坐标")]
    [SettingsDescription("左压合X轴坐标")]
    public double LeftX
    {
        get => (double)this[nameof(LeftX)];
        set => this[nameof(LeftX)] = value;
    }
    /// <summary>
    /// 左上下Z轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("左上下Z轴坐标")]
    [SettingsDescription("左上下Z轴坐标")]
    public double LeftZ
    {
        get => (double)this[nameof(LeftZ)];
        set => this[nameof(LeftZ)] = value;
    }
    /// <summary>
    /// 右压合X轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("右压合X轴坐标")]
    [SettingsDescription("右压合X轴坐标")]
    public double RightX
    {
        get => (double)this[nameof(RightX)];
        set => this[nameof(RightX)] = value;
    }
    /// <summary>
    /// 右上下Z轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("右上下Z轴坐标")]
    [SettingsDescription("右上下Z轴坐标")]
    public double RightZ
    {
        get => (double)this[nameof(RightZ)];
        set => this[nameof(RightZ)] = value;
    }
    /// <summary>
    /// 最大计量偏差值
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.00005")]
    [DisplayName("最大计量偏差值")]
    [SettingsDescription("最大计量偏差值")]
    public double MaxMeasureDev
    {
        get => (double)this[nameof(MaxMeasureDev)];
        set => this[nameof(MaxMeasureDev)] = value;
    }
}