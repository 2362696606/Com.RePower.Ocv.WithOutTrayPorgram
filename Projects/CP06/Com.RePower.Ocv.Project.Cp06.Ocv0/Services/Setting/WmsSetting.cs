using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting
{
    public class WmsSetting
    {
        /// <summary>
        /// 获取托盘条码接口
        /// </summary>
        public string GetBatteryInfoUrl { get; set; } = "RequetEquipmentStationBingAsset";
        /// <summary>
        /// 上传测试结果接口
        /// </summary>
        public string UploadTestResultUrl { get; set; } = "UploadOCVTestResult";
        /// <summary>
        /// 请求总完成到wms
        /// </summary>
        public string RequestAllocateCellToWmsUrl { get; set; } = "RequestAllocateCell";
        /// <summary>
        /// 基础地址
        /// </summary>
        public string BaseAddress { get; set; } = "http://172.17.20.6:8082/swagger/index.html/api/MERequest";
        /// <summary>
        /// 工站号
        /// </summary>
        public string RequestLocation { get; set; } = string.Empty;
        /// <summary>
        /// 库房编号
        /// </summary>
        public string WhCode { get; set; } = string.Empty;
        /// <summary>
        /// OCV0 OCV1 OCV2 OCV3
        /// </summary>
        public string DeviceName { get; set; } = "OCV0";
        ///// <summary>
        ///// 工艺号
        ///// </summary>
        //public string FileName { get; set; } = string.Empty;
        ///// <summary>
        ///// 工位号
        ///// </summary>
        //public string EquipmentCode { get; set; } = string.Empty;
    }
}
