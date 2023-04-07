using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public sealed class TestOption : ApplicationSettingsBase
    {
        /// <summary>
        /// 是否复测
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否复测")]
        [Description("是否复测")]
        public bool IsDoRetest
        {
            get => (bool)this[nameof(IsDoRetest)];
            set => this[nameof(IsDoRetest)] = value;
        }

        /// <summary>
        /// 是否上传到Mes
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否上传到Mes")]
        [Description("是否上传到Mes")]
        public bool IsDoUploadToMes
        {
            get => (bool)this[nameof(IsDoUploadToMes)];
            set => this[nameof(IsDoUploadToMes)] = value;
        }

        /// <summary>
        /// Msa测试次数
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0")]
        [DisplayName("Msa测试次数")]
        [Description("Msa测试次数")]
        public int MsaTestTimes
        {
            get => (int)this[nameof(MsaTestTimes)];
            set => this[nameof(MsaTestTimes)] = value;
        }

        /// <summary>
        /// 复测次数
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0")]
        [DisplayName("复测次数")]
        [Description("复测次数")]
        public int RetestTime
        {
            get => (int)this[nameof(RetestTime)];
            set => this[nameof(RetestTime)] = value;
        }
    }
}