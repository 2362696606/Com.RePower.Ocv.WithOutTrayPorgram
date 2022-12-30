using Autofac.Features.AttributeFilters;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Project.YiWei.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Controllers
{
    public class DevicesController
    {
        public DevicesController(IPlc localPlc
            , IDMM dMM
            , ISwitchBoard switchBoard
            , IOhm ohm)
        {
            LocalPlc = localPlc;
            DMM = dMM;
            SwitchBoard = switchBoard;
            Ohm = ohm;
            this.LocalPlcAddressCache = new Dictionary<string, string>();
            //this.LogisticsPlcAddressCache = new Dictionary<string, string>();
        }
        /// <summary>
        /// 本地Plc
        /// </summary>
        public IPlc LocalPlc { get; }
        /// <summary>
        /// 万用表
        /// </summary>
        public IDMM DMM { get; }
        public ISwitchBoard SwitchBoard { get; }
        public IOhm Ohm { get; }

        /// <summary>
        /// Plc地址映射字典
        /// </summary>
        public Dictionary<string,string> LocalPlcAddressCache { get; set; }
        //public Dictionary<string,string> LogisticsPlcAddressCache { get; set; }
    }
}
