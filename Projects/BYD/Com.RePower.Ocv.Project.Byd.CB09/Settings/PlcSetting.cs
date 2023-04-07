using System.ComponentModel;
using System.Configuration;
using Com.RePower.Ocv.Model.Entity;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class PlcSetting:ApplicationSettingsBase
{
    /// <summary>
    /// Ip地址
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("192.168.0.127")]
    [DisplayName("Ip地址")]
    [Description("Ip地址")]
    public string IpAddress
    {
        get => (string)this[nameof(IpAddress)];
        set => this[nameof(IpAddress)] = value;
    }

    /// <summary>
    /// 端口
    /// </summary>
    [UserScopedSetting]
    [DefaultSettingValue("502")]
    [DisplayName("端口")]
    [Description("端口")]
    public int Port
    {
        get => (int)this[nameof(Port)];
        set => this[nameof(Port)] = value;
    }
}