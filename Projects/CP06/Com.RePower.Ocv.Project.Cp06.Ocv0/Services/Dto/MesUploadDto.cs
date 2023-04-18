using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesUploadDto
    {
        /// <summary>
        /// 站点
        /// </summary>
        [JsonProperty("SITE")]
        public string Site { get; set; } = string.Empty;
        /// <summary>
        /// 物料编号
        /// </summary>
        [JsonProperty("ITEM_NO")]
        public string ItemNo { get; set; } = string.Empty;
        /// <summary>
        /// 工单编号
        /// </summary>
        [JsonProperty("SHOP_ORDER_NO")]
        public string ShopOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 工序编号
        /// </summary>
        [JsonProperty("OPERATION_NO")]
        public string OperationNo { get; set; } = string.Empty;
        /// <summary>
        /// 资源编号
        /// </summary>
        [JsonProperty("RESOURCE_NO")]
        public string ResourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 生产班次
        /// </summary>
        [JsonProperty("SHIFTS")]
        public string Shifts { get; set; } = string.Empty;
        /// <summary>
        /// 是否首检
        /// </summary>
        [JsonProperty("IS_FIRST_INSPECTION")]
        public string IsFirstInspection { get; set; } = string.Empty;
        /// <summary>
        /// 数据采集时间
        /// </summary>
        [JsonProperty("DC_DATE")]
        public string DcDate { get; set; } = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        [JsonProperty("DC_USER")]
        public string DcUser { get; set; } = string.Empty;
        /// <summary>
        /// 托盘号
        /// </summary>
        [JsonProperty("TRAY_NO")]
        public string TrayNo { get; set; } = string.Empty;
        /// <summary>
        /// ocv次数
        /// </summary>
        [JsonProperty("OCV_TIMES")]
        public string OcvTimes { get; set; } = string.Empty;
        /// <summary>
        /// 电池数据集合
        /// </summary>
        [JsonProperty("SFC_LIST")]
        public List<MesBatteryResultDto> SfcList { get; set; } = new List<MesBatteryResultDto>();
    }
}
