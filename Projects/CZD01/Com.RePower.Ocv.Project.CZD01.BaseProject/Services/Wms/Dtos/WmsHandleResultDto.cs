using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsHandleResultDto
    {
        /// <summary>
        /// OCV请求时的站台位置
        /// </summary>
        public string RequestLocation { get; set; } = string.Empty;
        /// <summary>
        /// 请求的托盘条码
        /// </summary>
        public string TrayCode { get; set; } = string.Empty;
        /// <summary>
        /// 任务号
        /// </summary>
        public long TaskCode { get; set; }
        /// <summary>
        /// 该站台应该要做的OCV流程名称，如：OCV1,OCV2,OCV3等
        /// </summary>
        public string Procedure { get; set; } = string.Empty;
        /// <summary>
        /// 托盘内的所有电芯信息
        /// </summary>
        public List<WmsBatteryInfo> BatteriesInfoList { get; set; } = new List<WmsBatteryInfo>();
    }
}
