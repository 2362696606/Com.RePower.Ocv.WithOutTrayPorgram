using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public sealed class TestOption : ApplicationSettingsBase
    {
        public static TestOption Default { get; } = ((TestOption)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new TestOption())));
        /// <summary>
        /// 是否复测
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否复测")]
        [SettingsDescription("是否复测")]
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
        [SettingsDescription("是否上传到Mes")]
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
        [SettingsDescription("Msa测试次数")]
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
        [SettingsDescription("复测次数")]
        public int RetestTime
        {
            get => (int)this[nameof(RetestTime)];
            set => this[nameof(RetestTime)] = value;
        }
        /// <summary>
        /// 是否测试电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试电压")]
        [SettingsDescription("是否测试电压")]
        public bool IsTestVol
        {
            get => (bool)this[nameof(IsTestVol)];
            set => this[nameof(IsTestVol)] = value;
        }
        /// <summary>
        /// 是否测试正极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试正极壳体电压")]
        [SettingsDescription("是否测试正极壳体电压")]
        public bool IsTestPVol
        {
            get => (bool)this[nameof(IsTestPVol)];
            set => this[nameof(IsTestPVol)] = value;
        }
        /// <summary>
        /// 是否测试负极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试负极壳体电压")]
        [SettingsDescription("是否测试负极壳体电压")]
        public bool IsTestNVol
        {
            get => (bool)this[nameof(IsTestNVol)];
            set => this[nameof(IsTestNVol)] = value;
        }
        /// <summary>
        /// 是否测试内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试内阻")]
        [SettingsDescription("是否测试内阻")]
        public bool IsTestRes
        {
            get => (bool)this[nameof(IsTestRes)];
            set => this[nameof(IsTestRes)] = value;
        }
        /// <summary>
        /// 是否测试温度
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试温度")]
        [SettingsDescription("是否测试温度")]
        public bool IsTestTemp
        {
            get => (bool)this[nameof(IsTestTemp)];
            set => this[nameof(IsTestTemp)] = value;
        }
        /// <summary>
        /// 是否正极测试温度
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否正极测试温度")]
        [SettingsDescription("是否正极测试温度")]
        public bool IsTestPTemp
        {
            get => (bool)this[nameof(IsTestPTemp)];
            set => this[nameof(IsTestPTemp)] = value;
        }
        /// <summary>
        /// 是否测试负极温度
        /// </summary>
        [ApplicationScopedSetting]
        [DefaultSettingValue("false")]
        [DisplayName("是否测试负极温度")]
        [SettingsDescription("是否测试负极温度")]
        public bool IsTestNTemp
        {
            get => (bool)this[nameof(IsTestNTemp)];
            set => this[nameof(IsTestNTemp)] = value;
        }
    }
}