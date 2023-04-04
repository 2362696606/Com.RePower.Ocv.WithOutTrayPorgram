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
        public string Site { get; set; } = string.Empty;
        /// <summary>
        /// 物料编号
        /// </summary>
        public string ItemNo { get; set; } = string.Empty;
        /// <summary>
        /// 工单编号
        /// </summary>
        public string ShopOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 工序编号
        /// </summary>
        public string OperationNo { get; set; } = string.Empty;
        /// <summary>
        /// 资源编号
        /// </summary>
        public string ResourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 生产班次
        /// </summary>
        public string Shifts { get; set; } = string.Empty;
        /// <summary>
        /// 是否首检
        /// </summary>
        public string IsFirstInspection { get; set; } = string.Empty;
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public string DcDate { get; set; } = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string DcUser { get; set; } = string.Empty;
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TrayNo { get; set; } = string.Empty;
        /// <summary>
        /// ocv次数
        /// </summary>
        public string OcvTimes { get; set; } = string.Empty;
        /// <summary>
        /// 电池数据集合
        /// </summary>
        public List<MesBatteryResultDto> SfcList { get; set; } = new List<MesBatteryResultDto>();
    }
}
