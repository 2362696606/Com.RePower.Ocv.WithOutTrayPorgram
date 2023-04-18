using System.ComponentModel;
using System.Configuration;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public sealed class BatteryStandard : ApplicationSettingsBase
    {
        public static BatteryStandard Default { get; } = ((BatteryStandard)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new BatteryStandard())));

        /// <summary>
        /// 最大电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大电压")]
        [SettingsDescription("最大电压")]
        [Description("最大电压")]
        public double MaxVol
        {
            get => (double)this[nameof(MaxVol)];
            set => this[nameof(MaxVol)] = value;
        }

        /// <summary>
        /// 最小电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小电压")]
        [SettingsDescription("最小电压")]
        public double MinVol
        {
            get => (double)this[nameof(MinVol)];
            set => this[nameof(MinVol)] = value;
        }

        /// <summary>
        /// 最大正极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大正极壳体电压")]
        [SettingsDescription("最大正极壳体电压")]
        public double MaxPVol
        {
            get => (double)this[nameof(MaxPVol)];
            set => this[nameof(MaxPVol)] = value;
        }

        /// <summary>
        /// 最小正极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小正极壳体电压")]
        [SettingsDescription("最小正极壳体电压")]
        public double MinPVol
        {
            get => (double)this[nameof(MinPVol)];
            set => this[nameof(MinPVol)] = value;
        }

        /// <summary>
        /// 最大负极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大负极壳体电压")]
        [SettingsDescription("最大负极壳体电压")]
        public double MaxNVol
        {
            get => (double)this[nameof(MaxNVol)];
            set => this[nameof(MaxNVol)] = value;
        }

        /// <summary>
        /// 最小负极壳体电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小负极壳体电压")]
        [SettingsDescription("最小负极壳体电压")]
        public double MinNVol
        {
            get => (double)this[nameof(MinNVol)];
            set => this[nameof(MinNVol)] = value;
        }

        /// <summary>
        /// 最大内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大内阻")]
        [SettingsDescription("最大内阻")]
        public double MaxRes
        {
            get => (double)this[nameof(MaxRes)];
            set => this[nameof(MaxRes)] = value;
        }

        /// <summary>
        /// 最小内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小内阻")]
        [SettingsDescription("最小内阻")]
        public double MinRes
        {
            get => (double)this[nameof(MinRes)];
            set => this[nameof(MinRes)] = value;
        }

        /// <summary>
        /// 最大温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大温度")]
        [SettingsDescription("最大温度")]
        public double MaxTemp
        {
            get => (double)this[nameof(MaxTemp)];
            set => this[nameof(MaxTemp)] = value;
        }

        /// <summary>
        /// 最小温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小温度")]
        [SettingsDescription("最小温度")]
        public double MinTemp
        {
            get => (double)this[nameof(MinTemp)];
            set => this[nameof(MinTemp)] = value;
        }

        /// <summary>
        /// 最大K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大K1")]
        [SettingsDescription("最大K1")]
        public double MaxK1
        {
            get => (double)this[nameof(MaxK1)];
            set => this[nameof(MaxK1)] = value;
        }

        /// <summary>
        /// /最小K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小K1")]
        [SettingsDescription("最小K1")]
        public double MinK1
        {
            get => (double)this[nameof(MinK1)];
            set => this[nameof(MinK1)] = value;
        }
    }
}