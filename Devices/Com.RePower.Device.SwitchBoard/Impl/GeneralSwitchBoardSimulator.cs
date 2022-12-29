using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.SwitchBoard.Impl
{
    public class GeneralSwitchBoardSimulator : GeneralSwitchBoardImpl
    {
        public GeneralSwitchBoardSimulator()
        {
            this.deviceBase = new SerialPortDeviceBaseSimulator();
        }
    }
}
