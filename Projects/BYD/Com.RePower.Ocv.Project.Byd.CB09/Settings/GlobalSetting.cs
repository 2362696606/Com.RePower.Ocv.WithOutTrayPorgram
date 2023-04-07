using Com.RePower.Ocv.Model.Enums;
using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public class GlobalSetting : ApplicationSettingsBase
    {
        /// <summary>
        /// Ocv类型
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("Ocv1")]
        [DisplayName("默认Ocv类型")]
        [Description("默认Ocv类型")]
        public OcvTypeEnum DefaultOcvType
        {
            get => (OcvTypeEnum)this[nameof(DefaultOcvType)];
            set => this[nameof(DefaultOcvType)] = value;
        }
    }
}