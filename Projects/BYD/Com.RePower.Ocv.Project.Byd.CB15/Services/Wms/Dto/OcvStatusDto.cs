using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class OcvStatusDto
    {
        /// <summary>
        /// 设备站台号（设备编码）
        /// </summary>
        public string EquipNum { get; set; } = string.Empty;
        /// <summary>
        /// 工作模式（1-手动，2-自动）
        /// </summary>
        public int WorkMode { get; set; }
        /// <summary>
        /// 设备状态（1-正常，2-故障）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 故障信息
        /// </summary>
        public string? ErrorMsg { get; set; }
    }
}
