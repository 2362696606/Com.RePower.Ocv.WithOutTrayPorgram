using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class NetDmmSetting:ApplicationSettingsBase
{
    public static NetDmmSetting Default { get; } = ((NetDmmSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new NetDmmSetting())));
    /// <summary>
    /// 设备名
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("Dmm")]
    [DisplayName("设备名")]
    [SettingsDescription("万用表设备名")]
    public string DeviceName => (string)this[nameof(DeviceName)];

    /// <summary>
    /// Ip地址
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("192.168.0.100")]
    [DisplayName("Ip地址")]
    [SettingsDescription("万用表Ip地址")]
    public string IpAddress => (string)this[nameof(IpAddress)];

    /// <summary>
    /// 端口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("502")]
    [DisplayName("端口")]
    [SettingsDescription("万用表端口号")]
    public int Port => (int)this[nameof(Port)];

    /// <summary>
    /// 读取延迟
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("200")]
    [DisplayName("读取延迟")]
    [SettingsDescription("万用表读取延迟")]
    public int ReadDelay => (int)this[nameof(ReadDelay)];
}