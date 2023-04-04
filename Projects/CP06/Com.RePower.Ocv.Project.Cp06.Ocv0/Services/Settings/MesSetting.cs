using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings
{
    public class MesSetting
    {
        /// <summary>
        /// 拆盘数据上传
        /// </summary>
        public string SfcTrayOnceUnbindUrl { get; set; } = string.Empty;
        public string SfcValidateDLcapacityAfterUrl { get; set; } = string.Empty;
        public string SaveDataAutoUrl { get; set; } = string.Empty;
        /// <summary>
        /// 上传设备状态
        /// </summary>
        public string UploadMachineStatusUrl { get; set; } = "http://10.10.1.240:8578/mes/third/thirdPartyAPI!doUploadMachineStatus_Change.action";
        public string? LoadShopOrderListUrl { get; set; } = "http://10.10.1.240:8578/mes/third/thirdPartyAPI!loadShopOrderList.action";

        /// <summary>
        /// 站点
        /// </summary>
        public string Site { get; set; } = string.Empty;
        /// <summary>
        /// 资源编号
        /// </summary>
        public string ResourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string DcUser { get; set; } = string.Empty;
        /// <summary>
        /// 基础地址
        /// </summary>
        public string? BaseAddress { get; internal set; }
    }
}
