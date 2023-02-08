using Com.RePower.DeviceBase.BaseDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Ohm.Impl.Hioki_BT3562
{
    public class Hioki_BT3562Simulator:Hioki_BT3562Impl
    {
        public Hioki_BT3562Simulator()
        {
            this.deviceBase = new SerialPortDeviceBaseSimulator();
            var dev = this.deviceBase as SerialPortDeviceBaseSimulator;
            dev!.RecoveryMethod = RecoveryMethod;
        }

        private byte[] RecoveryMethod(byte[] cmd)
        {

            var random = new Random();
            var randNum = random.NextDouble() * (0.001 - 0.0001) + 0.0001;
            var returnResult = Encoding.ASCII.GetBytes(randNum.ToString("f" + 6));
            return returnResult;
        }

        public override int ReadDelay { get ; set; }
    }
}
