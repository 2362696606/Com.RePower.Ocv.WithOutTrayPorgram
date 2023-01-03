using Com.RePower.DeviceBase.BaseDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public class Keysight_34461ASimulator:Keysight_34461AImpl
    {
        public Keysight_34461ASimulator()
        {
            this.netDeviceBase = new NetDeviceBaseSimulator();
            var dev = this.netDeviceBase as NetDeviceBaseSimulator;
            dev!.RecoveryMethod = RecoveryMethod;
        }

        private byte[] RecoveryMethod()
        {
            var random = new Random();
            var randNum = random.NextDouble() * (4000 - 2000) + 2000;
            var returnResult = Encoding.ASCII.GetBytes(randNum.ToString("f" + 6));
            return returnResult;
        }
    }
}
