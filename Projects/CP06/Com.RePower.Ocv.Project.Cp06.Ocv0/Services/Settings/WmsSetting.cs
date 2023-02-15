using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings
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
        public string RequestLocation { get; set; } = "FrontOcvStation:1_1_1_F01";
        /// <summary>
        /// 库房编号
        /// </summary>
        public string WhCode { get; set; } = "F";
        /// <summary>
        /// 请求位置
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string? ProjectCode { get; set; }
    }
}
