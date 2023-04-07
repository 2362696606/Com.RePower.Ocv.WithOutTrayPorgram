using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public class DevicesController
    {
        public DevicesController(IPlc localPlc
            , IDmm dMm
            , ISwitchBoard switchBoard)
        {
            LocalPlc = localPlc;
            Dmm = dMm;
            SwitchBoard = switchBoard;
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
        public IDmm Dmm { get; }

        public ISwitchBoard SwitchBoard { get; }

        /// <summary>
        /// Plc地址映射字典
        /// </summary>
        public Dictionary<string, string> LocalPlcAddressCache { get; set; }

        //public Dictionary<string,string> LogisticsPlcAddressCache { get; set; }
    }
}