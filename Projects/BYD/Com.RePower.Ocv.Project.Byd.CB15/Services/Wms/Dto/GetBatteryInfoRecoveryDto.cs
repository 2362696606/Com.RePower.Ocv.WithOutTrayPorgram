using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class GetBatteryInfoRecoveryDto
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipNum { get; set; } = string.Empty;
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string TrayCode { get; set; } = string.Empty;
        /// <summary>
        /// 工艺类别（1-OCV1，2-OCV2，3-OCV3）
        /// </summary>
        public string OprationType { get; set; } = string.Empty;
        /// <summary>
        /// 电芯类型
        /// </summary>
        public string? OprationVersion { get; set; }
        /// <summary>
        /// 电芯数据
        /// </summary>
        public List<TrayInfoResultDto>? TrayInfoLstResult { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public WmsRecoveryStatusDto Status { get; set; } = new WmsRecoveryStatusDto();
    }
}
