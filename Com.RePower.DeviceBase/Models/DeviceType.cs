using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        PLC,
        /// <summary>
        /// 万用表
        /// </summary>
        [Description("万用表")]
        DMM,
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
