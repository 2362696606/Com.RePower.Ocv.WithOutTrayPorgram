using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public class DevicesController
    {
        public DevicesController(IPlc localPlc
            , IDMM dMM
            , ISwitchBoard switchBoard)
        {
            Plc = localPlc;
            DMM = dMM;
            SwitchBoard = switchBoard;
            //this.PlcAddressCache = new Dictionary<string, string>();
            //this.LogisticsPlcAddressCache = new Dictionary<string, string>();
        }

        public IPlc Plc { get; }
        public IDMM DMM { get; }
        public ISwitchBoard SwitchBoard { get; }
    }
}
