using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting
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

        /// <summary>
        /// 站点
        /// </summary>
        public string SITE { get; set; } = string.Empty;
        /// <summary>
        /// 资源编号
        /// </summary>
        public string RESOURCE_NO { get; set; } = string.Empty;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string DC_USER { get; set; } = string.Empty;
        /// <summary>
        /// 基础地址
        /// </summary>
        public string? BaseAddress { get; internal set; }
    }
}
