using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    public class BatteryDto
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 电池位置
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public double VolValue { get; set; }
        /// <summary>
        /// 正极壳电压
        /// </summary>
        public double PVolValue { get; set; }
        /// <summary>
        /// 负极壳电压
        /// </summary>
        public double NVolValue { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public double Res { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public double Temp { get; set; }
        /// <summary>
        /// 正极温度
        /// </summary>
        public double PTemp { get; set; }
        /// <summary>
        /// 负极温度
        /// </summary>
        public double NTemp { get; set; }
        /// <summary>
        /// K值
        /// </summary>
        public double KValue { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        public int BatteryType { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
    }
}
