using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Excel
{
    public class ExcelSaveDto
    {
        /// <summary>
        /// 位置
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 电压
        /// </summary>
        public double VolValue { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public double ResValue { get; set; }
        /// <summary>
        /// 负极壳体电压
        /// </summary>
        public double NVolValue { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string NgResult { get; set; } = string.Empty;
        /// <summary>
        /// ng描述
        /// </summary>
        public string NgDescription { get; set; } = string.Empty;
    }
}
