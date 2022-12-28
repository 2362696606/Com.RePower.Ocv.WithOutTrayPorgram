using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Models
{
    public class RnDbOcv 
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string? EqpId { get; set; }
        /// <summary>
        /// 设备号加几线  ocv4-3
        /// </summary>
        public string? PcId { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string Operation { get; set; } = null!;
        /// <summary>
        /// 是否跨线 1默认
        /// </summary>
        public decimal IsTrans { get; set; }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TrayId { get; set; } = null!;
        /// <summary>
        /// 电芯码
        /// </summary>
        public string CellId { get; set; } = null!;
        /// <summary>
        ///  通道号   电池位置
        /// </summary>
        public int BatteryPos { get; set; }
        /// <summary>
        /// ocv304  设备号加几线
        /// </summary>
        public string? ModelNo { get; set; }
        /// <summary>
        /// 默认null
        /// </summary>
        public string? BatchNo { get; set; }
        /// <summary>
        /// 托盘测试结果 
        /// </summary>
        public string? TotalNgState { get; set; }
        /// <summary>
        /// 电池电压
        /// </summary>
        public decimal OcvVoltage { get; set; }
        /// <summary>
        /// Acir
        /// </summary>
        public decimal? Acir { get; set; }
        /// <summary>
        /// 电池ng原因（结束代码）
        /// </summary>
        public string? TestNgCode { get; set; }
        /// <summary>
        /// 电池测试结果，ng，ok
        /// </summary>
        public string? TestResult { get; set; }
        /// <summary>
        /// 结果描述，
        /// </summary>
        public string? TestResultDesc { get; set; }
        /// <summary>
        /// 正极测试电压
        /// </summary>
        public decimal? PostiveShellVoltage { get; set; }
        /// <summary>
        /// 正极壳体结束代码，默认00
        /// </summary>
        public string? PostiveSvNgCode { get; set; }
        /// <summary>
        /// 正极测试结果，ng，ok
        /// </summary>
        public string? PostiveSvResult { get; set; }
        /// <summary>
        /// 正极测试结果描述，合格、不合格
        /// </summary>
        public string? PostiveSvResultDesc { get; set; }
        /// <summary>
        /// 负极壳体电压
        /// </summary>
        public decimal? ShellVoltage { get; set; }
        /// <summary>
        /// 负极壳体结束代码，C1（超下限）C2（超上限）00（测试OK）
        /// </summary>
        public string? SvNgCode { get; set; }
        /// <summary>
        /// 负极测试结果，ng、ok
        /// </summary>
        public string? SvResult { get; set; }
        /// <summary>
        /// 负极测试结果描述  合格、不合格
        /// </summary>
        public string? SvResultDesc { get; set; }
        /// <summary>
        /// 正极温度
        /// </summary>
        public decimal? PostiveTemp { get; set; }
        /// <summary>
        /// 负极温度
        /// </summary>
        public decimal? NegativeTemp { get; set; }

        public decimal? K { get; set; }

        public decimal? VDrop { get; set; }

        public decimal? VDropRange { get; set; }

        public string? VDropRangeCode { get; set; }

        public string? VDropResult { get; set; }

        public string? VDropResultDesc { get; set; }
        /// <summary>
        /// ACIR极差
        /// </summary>
        public decimal? AcirRange { get; set; }
        /// <summary>
        /// 内阻结束代码，AX1（超下限），AX2（超上限）00（测试OK）
        /// </summary>
        public string? RRangeNgCode { get; set; }
        /// <summary>
        /// 内阻测试结果  ok、ng
        /// </summary>
        public string? RRangeResult { get; set; }
        /// <summary>
        /// 内阻判定描述   合格、测试不合格
        /// </summary>
        public string? RRangeResultDesc { get; set; }
        /// <summary>
        /// 待确定（暂定测试电压）
        /// </summary>
        public decimal? RevOcv { get; set; }

        public decimal? Capacity { get; set; }
        /// <summary>
        /// 流程结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 生成数据时间
        /// </summary>
        public DateTime? InsertTime { get; set; }
        /// <summary>
        /// 自动手动
        /// </summary>
        public string TestMode { get; set; } = null!;

        public decimal? DischargeTime { get; set; }
    }
}
