using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class SerialPortSwitchBoardSetting:ApplicationSettingsBase
{
    public static SerialPortSwitchBoardSetting Default { get; } = ((SerialPortSwitchBoardSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SerialPortSwitchBoardSetting())));
    /// <summary>
    /// 设备名
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("SwitchBoard")]
    [DisplayName("设备名")]
    [SettingsDescription("切换版设备名")]
    public string DeviceName => (string)this[nameof(DeviceName)];

    /// <summary>
    /// COM口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("COM2")]
    [DisplayName("COM口")]
    [SettingsDescription("切换版COM口")]
    public string PortName => (string)this[nameof(PortName)];

    /// <summary>
    /// 波特率
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("9600")]
    [DisplayName("波特率")]
    [SettingsDescription("切换版波特率")]
    public int BaudRate => (int)this[nameof(BaudRate)];

    /// <summary>
    /// 读取延迟
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("200")]
    [DisplayName("读取延迟")]
    [SettingsDescription("切换版读取延迟")]
    public int ReadDelay => (int)this[nameof(ReadDelay)];
}