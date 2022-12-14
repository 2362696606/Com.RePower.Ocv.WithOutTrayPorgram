using Autofac.Features.AttributeFilters;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Project.WuWei.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public class DevicesController
    {
        public DevicesController([KeyFilter(PlcDeviceEnum.LocalPlc)]IPlc localPlc, [KeyFilter(PlcDeviceEnum.LogisticsPlc)]IPlc logisticsPlc,IDMM dMM)
        {
            LocalPlc = localPlc;
            LogisticsPlc = logisticsPlc;
            DMM = dMM;
            this.LocalPlcAddressCache = new Dictionary<string, string>();
            this.LogisticsPlcAddressCache = new Dictionary<string, string>();
        }
        /// <summary>
        /// 本地Plc
        /// </summary>
        public IPlc LocalPlc { get; }
        /// <summary>
        /// 物流Plc
        /// </summary>
        public IPlc LogisticsPlc { get; }
        /// <summary>
        /// 万用表
        /// </summary>
        public IDMM DMM { get; }
        /// <summary>
        /// Plc地址映射字典
        /// </summary>
        public Dictionary<string,string> LocalPlcAddressCache { get; set; }
        public Dictionary<string,string> LogisticsPlcAddressCache { get; set; }
    }
}
