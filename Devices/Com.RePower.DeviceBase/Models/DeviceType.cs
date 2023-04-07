using System.ComponentModel;

namespace Com.RePower.DeviceBase.Models
{
    /// <summary>
    /// 设备类型枚举
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 0,

        /// <summary>
        /// PLC
        /// </summary>
        [Description("PLC")]
        Plc,

        /// <summary>
        /// 万用表
        /// </summary>
        [Description("万用表")]
        Dmm,

        /// <summary>
        /// 内阻仪
        /// </summary>
        [Description("内阻仪")]
        Ohm,

        /// <summary>
        /// 切换板
        /// </summary>
        [Description("切换板")]
        SwitchBoard,

        /// <summary>
        /// 温度传感器
        /// </summary>
        [Description("温度传感器")]
        TemperatureSensor,
    }
}