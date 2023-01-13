using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesUploadDto
    {
        /// <summary>
        /// 站点
        /// </summary>
        public string SITE { get; set; } = string.Empty;
        /// <summary>
        /// 物料编号
        /// </summary>
        public string ITEM_NO { get; set; } = string.Empty;
        /// <summary>
        /// 工单编号
        /// </summary>
        public string SHOP_ORDER_NO { get; set; } = string.Empty;
        /// <summary>
        /// 工序编号
        /// </summary>
        public string OPERATION_NO { get; set; } = string.Empty;
        /// <summary>
        /// 资源编号
        /// </summary>
        public string RESOURCE_NO { get; set; } = string.Empty;
        /// <summary>
        /// 生产班次
        /// </summary>
        public string SHIFTS { get; set; } = string.Empty;
        /// <summary>
        /// 是否首检
        /// </summary>
        public string IS_FIRST_INSPECTION { get; set; } = string.Empty;
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public string DC_DATE { get; set; } = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string DC_USER { get; set; } = string.Empty;
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TRAY_NO { get; set; } = string.Empty;
        /// <summary>
        /// ocv次数
        /// </summary>
        public string OCV_TIMES { get; set; } = string.Empty;
        /// <summary>
        /// 电池数据集合
        /// </summary>
        public List<MesBatteryResultDto> SFC_LIST { get; set; } = new List<MesBatteryResultDto>();
    }
}
