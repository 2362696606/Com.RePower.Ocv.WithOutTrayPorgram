using Com.RePower.Ocv.Model.CommonModel;
using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public class TestOption : SingletonApplicationSettingsBase<TestOption>
    {
        /// <summary>
        /// 是否复测
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否复测")]
        [Description("是否复测")]
        public bool IsDoRetest { get; set; }

        /// <summary>
        /// 是否上传到Mes
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否上传到Mes")]
        [Description("是否上传到Mes")]
        public bool IsDoUploadToMes { get; set; }

        /// <summary>
        /// 复测次数
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0")]
        [DisplayName("复测次数")]
        [Description("复测次数")]
        public int RetestTime { get; set; }

        /// <summary>
        /// Msa测试次数
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0")]
        [DisplayName("Msa测试次数")]
        [Description("Msa测试次数")]
        public int MsaTestTime { get; set; }
    }
}