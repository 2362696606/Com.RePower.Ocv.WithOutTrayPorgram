using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class SerialPortOhmSetting:ApplicationSettingsBase
{
    public static SerialPortOhmSetting Default { get; } = ((SerialPortOhmSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SerialPortOhmSetting())));
    /// <summary>
    /// 设备名
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("Ohm")]
    [DisplayName("设备名")]
    [SettingsDescription("内阻仪设备名")]
    public string DeviceName => (string)this[nameof(DeviceName)];

    /// <summary>
    /// COM口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("COM1")]
    [DisplayName("COM口")]
    [SettingsDescription("内阻仪COM口")]
    public string PortName => (string)this[nameof(PortName)];

    /// <summary>
    /// 波特率
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("19200")]
    [DisplayName("波特率")]
    [SettingsDescription("内阻仪波特率")]
    public int BaudRate => (int)this[nameof(BaudRate)];

    /// <summary>
    /// 读取延迟
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("200")]
    [DisplayName("读取延迟")]
    [SettingsDescription("内阻仪读取延迟")]
    public int ReadDelay => (int)this[nameof(ReadDelay)];
}