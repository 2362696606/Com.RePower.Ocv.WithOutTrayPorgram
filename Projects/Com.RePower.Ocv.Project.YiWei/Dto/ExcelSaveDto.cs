using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Dto
{
    public class ExcelSaveDto
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
        /// 电池类型
        /// </summary>
        public int? BatteryType { get; set; }
        /// <summary>
        /// Ocv类型
        /// </summary>
        public string? OcvType { get; set; }
        /// <summary>
        /// Ocv工站名
        /// </summary>
        public string? OcvStationName { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public double? VolValue { get; set; }
        /// <summary>
        /// 正极壳电压
        /// </summary>
        public double? PVolValue { get; set; }
        /// <summary>
        /// 负极壳电压
        /// </summary>
        public double? NVolValue { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public double? Res { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public double? Temp { get; set; }
        /// <summary>
        /// 正极温度
        /// </summary>
        public double? PTemp { get; set; }
        /// <summary>
        /// 负极温度
        /// </summary>
        public double? NTemp { get; set; }
        /// <summary>
        /// mg描述
        /// </summary>
        public string? NgDescription { get; set; }
        /// <summary>
        /// 是否ng
        /// </summary>
        public bool IsNg { get; set; } = false;
        ///// <summary>
        ///// K值1
        ///// </summary>
        //public double? KValue1 { get; set; }
        ///// <summary>
        ///// K值2
        ///// </summary>
        //public double? KValue2 { get; set; }
        ///// <summary>
        ///// K值3
        ///// </summary>
        //public double? KValue3 { get; set; }
        ///// <summary>
        ///// K值4
        ///// </summary>
        //public double? KValue4 { get; set; }
        ///// <summary>
        ///// K值5
        ///// </summary>
        //public double? KValue5 { get; set; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string? TrayCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public string TestTime { get; set; } = DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss");
        /// <summary>
        /// 任务号
        /// </summary>
        public long? TaskCode { get; set; }
    }
}
