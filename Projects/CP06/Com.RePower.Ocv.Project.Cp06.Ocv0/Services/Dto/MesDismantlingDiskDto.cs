using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesDismantlingDiskDto
    {
        /// <summary>
        /// 站点（固定，可配置）
        /// </summary>
        public string Site { get; set; } = string.Empty;
        /// <summary>
        /// MES账号
        /// </summary>
        public string DcUser { get; set; } = string.Empty;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string ResourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 托盘编号
        /// </summary>
        public string TrayNo { get; set; } = string.Empty;
        /// <summary>
        /// 工单号
        /// </summary>
        public string ShopOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 是否首检
        /// </summary>
        public string IsFirstInspection { get; set; } = string.Empty;
    }
}
