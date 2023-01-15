using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    public class BatteryDto
    {
        public long Id { get; set; }
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
        public string? OcvStationName{ get; set; }
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
        /// K值1
        /// </summary>
        public double? KValue1 { get; set; }
        /// <summary>
        /// K值2
        /// </summary>
        public double? KValue2 { get; set; }
        /// <summary>
        /// K值3
        /// </summary>
        public double? KValue3 { get; set; }
        /// <summary>
        /// K值4
        /// </summary>
        public double? KValue4 { get; set; }
        /// <summary>
        /// K值5
        /// </summary>
        public double? KValue5 { get; set; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string? TrayCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
        /// <summary>
        /// 保留int类型1
        /// </summary>
        public int? ReserveInt1 { get; set; }
        /// <summary>
        /// 保留int类型2
        /// </summary>
        public int? ReserveInt2 { get; set; }
        /// <summary>
        /// 保留int类型3
        /// </summary>
        public int? ReserveInt3 { get; set; }
        /// <summary>
        /// 保留int类型4
        /// </summary>
        public int? ReserveInt4 { get; set; }
        /// <summary>
        /// 保留int类型5
        /// </summary>
        public int? ReserveInt5 { get; set; }
        /// <summary>
        /// 保留double类型1
        /// </summary>
        public double? ReserveValue1 { get; set; }
        /// <summary>
        /// 保留double类型2
        /// </summary>
        public double? ReserveValue2 { get; set; }
        /// <summary>
        /// 保留double类型3
        /// </summary>
        public double? ReserveValue3 { get; set; }
        /// <summary>
        /// 保留double类型4
        /// </summary>
        public double? ReserveValue4 { get; set; }
        /// <summary>
        /// 保留double类型5
        /// </summary>
        public double? ReserveValue5 { get; set; }
        /// <summary>
        /// 保留string类型1
        /// </summary>
        public string? ReserveText1 { get; set; }
        /// <summary>
        /// 保留string类型2
        /// </summary>
        public string? ReserveText2 { get; set; }
        /// <summary>
        /// 保留string类型3
        /// </summary>
        public string? ReserveText3 { get; set; }
        /// <summary>
        /// 保留string类型4
        /// </summary>
        public string? ReserveText4 { get; set; }
        /// <summary>
        /// 保留string类型5
        /// </summary>
        public string? ReserveText5 { get; set; }
        /// <summary>
        /// 保留时间1
        /// </summary>
        public DateTime? ReserveTime1 { get; set; }
        /// <summary>
        /// 保留时间2
        /// </summary>
        public DateTime? ReserveTime2 { get; set; }
        /// <summary>
        /// 保留时间3
        /// </summary>
        public DateTime? ReserveTime3 { get; set; }
        /// <summary>
        /// 保留时间4
        /// </summary>
        public DateTime? ReserveTime4 { get; set; }
        /// <summary>
        /// 保留时间5
        /// </summary>
        public DateTime? ReserveTime5 { get; set; }
    }
}
