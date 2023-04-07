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
        [Description("最大电压")]
        public Decimal MaxVol
        {
            get => (decimal)this[nameof(MaxVol)];
            set => this[nameof(MaxVol)] = value;
        }

        /// <summary>
        /// 最小电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小电压")]
        [Description("最小电压")]
        public Decimal MinVol
        {
            get => (decimal)this[nameof(MinVol)];
            set => this[nameof(MinVol)] = value;
        }

        /// <summary>
        /// 最大内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大内阻")]
        [Description("最大内阻")]
        public Decimal MaxRes
        {
            get => (decimal)this[nameof(MaxRes)];
            set => this[nameof(MaxRes)] = value;
        }

        /// <summary>
        /// 最小内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小内阻")]
        [Description("最小内阻")]
        public Decimal MinRes
        {
            get => (decimal)this[nameof(MinRes)];
            set => this[nameof(MinRes)] = value;
        }

        /// <summary>
        /// 最大温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大温度")]
        [Description("最大温度")]
        public Decimal MaxTemp
        {
            get => (decimal)this[nameof(MaxTemp)];
            set => this[nameof(MaxTemp)] = value;
        }

        /// <summary>
        /// 最小温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小温度")]
        [Description("最小温度")]
        public Decimal MinTemp
        {
            get => (decimal)this[nameof(MinTemp)];
            set => this[nameof(MinTemp)] = value;
        }

        /// <summary>
        /// 最大K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大K1")]
        [Description("最大K1")]
        public Decimal MaxK1
        {
            get => (decimal)this[nameof(MaxK1)];
            set => this[nameof(MaxK1)] = value;
        }

        /// <summary>
        /// /最小K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小K1")]
        [Description("最小K1")]
        public Decimal MinK1
        {
            get => (decimal)this[nameof(MinK1)];
            set => this[nameof(MinK1)] = value;
        }
    }
}