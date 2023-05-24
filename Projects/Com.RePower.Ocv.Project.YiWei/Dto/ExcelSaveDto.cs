using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npoi.Mapper.Attributes;

namespace Com.RePower.Ocv.Project.YiWei.Dto
{
    public class ExcelSaveDto
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        //[Column("电芯条码")]
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 电池位置
        /// </summary>
        //[Column("电池位置")]
        public int Position { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        //[Column("电池类型")]
        public int? BatteryType { get; set; }
        /// <summary>
        /// Ocv类型
        /// </summary>
        //[Column("Ocv类型")]
        public string? OcvType { get; set; }
        /// <summary>
        /// Ocv工站名
        /// </summary>
        //[Column("Ocv工站名")]
        public string? OcvStationName { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        //[Column("电压")]
        public double? VolValue { get; set; }
        /// <summary>
        /// 正极壳电压
        /// </summary>
        //[Column("正极壳体电压")]
        public double? PVolValue { get; set; }
        /// <summary>
        /// 负极壳电压
        /// </summary>
        //[Column("负极壳体电压")]
        public double? NVolValue { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        //[Column("内阻")]
        public double? Res { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        //[Column("温度")]
        public double? Temp { get; set; }
        /// <summary>
        /// 正极温度
        /// </summary>
        //[Column("正极温度")]
        public double? PTemp { get; set; }
        /// <summary>
        /// 负极温度
        /// </summary>
        //[Column("负极温度")]
        public double? NTemp { get; set; }
        /// <summary>
        /// ng描述
        /// </summary>
        //[Column("ng描述")]
        public string? NgDescription { get; set; }
        /// <summary>
        /// 是否ng
        /// </summary>
        //[Column("是否ng")]
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
        //[Column("托盘条码")]
        public string? TrayCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        //[Column("测试时间")]
        public string TestTime { get; set; } = DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss");
        /// <summary>
        /// 任务号
        /// </summary>
        //[Column("任务号")]
        public long? TaskCode { get; set; }
    }
}
