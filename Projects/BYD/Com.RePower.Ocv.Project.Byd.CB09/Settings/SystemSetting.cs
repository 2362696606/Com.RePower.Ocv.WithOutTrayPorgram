using Com.RePower.Ocv.Model.Enums;
using System.ComponentModel;
using System.Configuration;
using Bluegrams.Application;
using Com.RePower.Ocv.Project.Byd.CB09.Models;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    //[SettingsProvider(typeof(PortableJsonSettingsProvider))]
    public class SystemSetting : ApplicationSettingsBase
    {
        public static SystemSetting Default { get; } = ((SystemSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SystemSetting())));
        /// <summary>
        /// Ocv类型
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("OCV1")]
        [DisplayName("默认Ocv类型")]
        [SettingsDescription("默认Ocv类型")]
        public OcvTypeEnum DefaultOcvType => (OcvTypeEnum)this[nameof(DefaultOcvType)];
        /// <summary>
        /// 工作类型
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("FourBars")]
        [DisplayName("默认工作类型")]
        [SettingsDescription("默认工作类型")]
        public WorkTypeEnum DefaultWorkType => (WorkTypeEnum)this[nameof(DefaultWorkType)];
    }
}