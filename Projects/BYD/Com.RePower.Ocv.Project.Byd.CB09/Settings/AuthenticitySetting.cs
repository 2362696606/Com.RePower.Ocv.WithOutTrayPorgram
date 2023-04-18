using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class AuthenticitySetting : ApplicationSettingsBase
{
    public static AuthenticitySetting Default { get; } = ((AuthenticitySetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new AuthenticitySetting())));
    /// <summary>
    /// 真实Plc
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实Plc")]
    [SettingsDescription("是否是真是的Plc")]
    public bool IsRealPlc
    {
        get => (bool)this[nameof(IsRealPlc)];
        set => this[nameof(IsRealPlc)] = value;
    }
    /// <summary>
    /// 真实万用表
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实万用表")]
    [SettingsDescription("是否是真实万用表")]
    public bool IsRealDmm
    {
        get => (bool)this[nameof(IsRealDmm)];
        set => this[nameof(IsRealDmm)] = value;
    }
    /// <summary>
    /// 真实内阻仪
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实内阻仪")]
    [SettingsDescription("是否是真实内阻仪")]
    public bool IsRealOhm
    {
        get => (bool)this[nameof(IsRealOhm)];
        set => this[nameof(IsRealOhm)] = value;
    }
    /// <summary>
    /// 真实切换版
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实切换版")]
    [SettingsDescription("是否是真实切换版")]
    public bool IsRealSwitchBoard
    {
        get => (bool)this[nameof(IsRealSwitchBoard)];
        set => this[nameof(IsRealSwitchBoard)] = value;
    }
    /// <summary>
    /// 真实调度
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实调度")]
    [SettingsDescription("是否是真实调度")]
    public bool IsRealWms
    {
        get => (bool)this[nameof(IsRealWms)];
        set => this[nameof(IsRealWms)] = value;
    }
    /// <summary>
    /// 真实Mes
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("false")]
    [DisplayName("真实Mes")]
    [SettingsDescription("是否是真实Mes")]
    public bool IsRealMes
    {
        get => (bool)this[nameof(IsRealMes)];
        set => this[nameof(IsRealMes)] = value;
    }
}