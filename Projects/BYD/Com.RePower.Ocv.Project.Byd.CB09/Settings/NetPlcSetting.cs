using System.ComponentModel;
using System.Configuration;
using Com.RePower.Ocv.Project.Byd.CB09.Models;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class NetPlcSetting:ApplicationSettingsBase
{
    public static NetPlcSetting Default { get; } = ((NetPlcSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new NetPlcSetting())));
    /// <summary>
    /// 设备名
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("Plc")]
    [DisplayName("设备名")]
    [SettingsDescription("设备名")]
    public string DeviceName => (string)this[nameof(DeviceName)];

    /// <summary>
    /// Ip地址
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("192.168.0.127")]
    [DisplayName("Ip地址")]
    [SettingsDescription("PlcIp地址")]
    public string IpAddress => (string)this[nameof(IpAddress)];

    /// <summary>
    /// 端口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("502")]
    [DisplayName("端口")]
    [SettingsDescription("Plc端口号")]
    public int Port => (int)this[nameof(Port)];

    /// <summary>
    /// DataFormat
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("ABCD")]
    [DisplayName("DataFormat")]
    [SettingsDescription("DataFormat")]
    public string DataFormat => (string)this[nameof(DataFormat)];
}