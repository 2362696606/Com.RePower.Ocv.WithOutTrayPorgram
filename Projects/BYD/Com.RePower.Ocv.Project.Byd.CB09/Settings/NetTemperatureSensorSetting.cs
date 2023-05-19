using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public class NetTemperatureSensorSetting: ApplicationSettingsBase
    {
        public static NetTemperatureSensorSetting Default { get; } = ((NetTemperatureSensorSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new NetTemperatureSensorSetting())));

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
}
