using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class WmsUploadResultRequestDto
    {
        /// <summary>
        /// 库房编号
        /// </summary>
        public string WhCode { get; set; } = string.Empty;
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string TrayBarcode { get; set; } = string.Empty;
        /// <summary>
        /// 位置
        /// </summary>
        public string DeviceName { get; set; } = "OCV0";
        /// <summary>
        /// 电池结果内容信息
        /// </summary>
        public List<WmsBatteryResultContent> BatteryResultContent { get; set; } = new List<WmsBatteryResultContent>();
        /// <summary>
        /// 工序OCV0 OCV1 OCV2 OCV3
        /// </summary>
        public string Procedure { get; set; } = "OCV0";
    }
}
