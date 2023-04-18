using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

public class WmsSetting: ApplicationSettingsBase
{
    public static WmsSetting Default { get; } = ((WmsSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new WmsSetting())));
    /// <summary>
    /// 获取托盘条码接口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("OcvGetTrayInfo")]
    [DisplayName("获取托盘条码接口")]
    [SettingsDescription("获取托盘条码接口")]
    public string GetBatteryInfoUrl => (string)this[nameof(GetBatteryInfoUrl)];

    /// <summary>
    /// 上传测试结果接口
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("UploadOcvResult")]
    [DisplayName("上传测试结果接口")]
    [SettingsDescription("上传测试结果接口")]
    public string UploadTestResultUrl => (string)this[nameof(UploadTestResultUrl)];

    /// <summary>
    /// 基础地址
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("http://172.17.20.6:8082/swagger/index.html/api/MERequest")]
    [DisplayName("基础地址")]
    [SettingsDescription("基础地址")]
    [SpecialSetting(SpecialSetting.WebServiceUrl)]
    public string BaseAddress => (string)this[nameof(BaseAddress)];

    /// <summary>
    /// 工艺号
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("")]
    [DisplayName("工艺号")]
    [SettingsDescription("工艺号")]
    public string FileName => (string)this[nameof(FileName)];

    /// <summary>
    /// 工位号
    /// </summary>
    [ApplicationScopedSetting]
    [DefaultSettingValue("")]
    [DisplayName("工位号")]
    [SettingsDescription("工位号")]
    public string EquipmentCode => (string)this[nameof(EquipmentCode)];
}