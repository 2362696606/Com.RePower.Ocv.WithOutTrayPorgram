using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class WmsBatteryResultContent
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string Barcode { get; set; } = string.Empty;
        /// <summary>
        /// 测试结果
        /// </summary>
        public string TestResult { get; set; } = "OK";
        /// <summary>
        /// 电池托盘位置
        /// </summary>
        public int Index { get; set; }
    }
}
