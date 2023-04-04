using Com.RePower.Ocv.Model.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings
{
    public class BatteryStandard : SingletonApplicationSettingsBase<BatteryStandard>
    {
        /// <summary>
        /// 最大电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大电压")]
        [Description("最大电压")]
        public Decimal MaxVol { get; set; }
        /// <summary>
        /// 最小电压
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小电压")]
        [Description("最小电压")]
        public Decimal MinVol { get; set; }
        /// <summary>
        /// 最大内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大内阻")]
        [Description("最大内阻")]
        public Decimal MaxRes { get; set; }
        /// <summary>
        /// 最小内阻
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小内阻")]
        [Description("最小内阻")]
        public Decimal MinRes { get; set; }
        /// <summary>
        /// 最大温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大温度")]
        [Description("最大温度")]
        public Decimal MaxTemp { get; set; }
        /// <summary>
        /// 最小温度
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小温度")]
        [Description("最小温度")]
        public Decimal MinTemp { get; set; }
        /// <summary>
        /// 最大K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最大K1")]
        [Description("最大K1")]
        public Decimal MaxK1 { get; set; }
        /// <summary>
        /// /最小K1
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("0.0")]
        [DisplayName("最小K1")]
        [Description("最小K1")]
        public Decimal MinK1 { get; set; }
    }
}
