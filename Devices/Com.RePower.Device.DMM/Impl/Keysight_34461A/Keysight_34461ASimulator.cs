using Com.RePower.DeviceBase.BaseDevice;
using System.Text;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public class Keysight34461ASimulator : Keysight34461AImpl
    {
        public Keysight34461ASimulator()
        {
            this.NetDeviceBase = new NetDeviceBaseSimulator();
            var dev = this.NetDeviceBase as NetDeviceBaseSimulator;
            dev!.RecoveryMethod = RecoveryMethod;
        }

        private byte[] RecoveryMethod(byte[] cmd)
        {
            var random = new Random();
            var randNum = random.NextDouble() * (4.0 - 2.0) + 2.0;
            var returnResult = Encoding.ASCII.GetBytes(randNum.ToString("f" + 6));
            return returnResult;
        }
    }
}