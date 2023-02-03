using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class WmsRecoveryStatusDto
    {
        /// <summary>
        /// 0=成功; 1=失败;
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// ReturnCode对应的详细说明。如成功，不用提供说明；否则需提供错误信息。
        /// </summary>
        public string? Message { get; set; }
    }
}
