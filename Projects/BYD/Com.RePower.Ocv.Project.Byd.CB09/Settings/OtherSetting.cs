using System.ComponentModel;
using System.Configuration;

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
    public decimal LeftX
    {
        get => (decimal)this[nameof(LeftX)];
        set => this[nameof(LeftX)] = value;
    }
    /// <summary>
    /// 左上下Z轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("左上下Z轴坐标")]
    [SettingsDescription("左上下Z轴坐标")]
    public decimal LeftZ
    {
        get => (decimal)this[nameof(LeftZ)];
        set => this[nameof(LeftZ)] = value;
    }
    /// <summary>
    /// 右压合X轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("右压合X轴坐标")]
    [SettingsDescription("右压合X轴坐标")]
    public decimal RightX
    {
        get => (decimal)this[nameof(RightX)];
        set => this[nameof(RightX)] = value;
    }
    /// <summary>
    /// 右上下Z轴坐标
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("0.0")]
    [DisplayName("右上下Z轴坐标")]
    [SettingsDescription("右上下Z轴坐标")]
    public decimal RightZ
    {
        get => (decimal)this[nameof(RightZ)];
        set => this[nameof(RightZ)] = value;
    }
}