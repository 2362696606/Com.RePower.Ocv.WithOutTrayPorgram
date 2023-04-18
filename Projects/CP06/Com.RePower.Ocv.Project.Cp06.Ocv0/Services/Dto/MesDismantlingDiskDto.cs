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
        public string SITE { get; set; } = string.Empty;
        /// <summary>
        /// MES账号
        /// </summary>
        public string DC_USER { get; set; } = string.Empty;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string RESOURCE_NO { get; set; } = string.Empty;
        /// <summary>
        /// 托盘编号
        /// </summary>
        public string TRAY_NO { get; set; } = string.Empty;
        /// <summary>
        /// 工单号
        /// </summary>
        public string SHOP_ORDER_NO { get; set; } = string.Empty;
        /// <summary>
        /// 是否首检
        /// </summary>
        public string IS_FIRST_INSPECTION { get; set; } = string.Empty;
    }
}
