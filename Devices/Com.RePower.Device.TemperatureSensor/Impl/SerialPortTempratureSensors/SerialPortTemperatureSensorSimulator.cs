using Com.RePower.DeviceBase.BaseDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor.Impl.SerialPortTempratureSensors
{
    public class SerialPortTemperatureSensorSimulator : SerialPortTemperatureSensorImpl
    {
        public SerialPortTemperatureSensorSimulator()
        {
            this.serialPortDeviceBase = new SerialPortDeviceBaseSimulator();
            if(serialPortDeviceBase is SerialPortDeviceBaseSimulator tempDeviceBase)
            {
                tempDeviceBase.RecoveryMethod = RecoveryMethod;
            }
        }

        private byte[] RecoveryMethod(byte[] arg)
        {
            throw new NotImplementedException();
        }
    }
}
