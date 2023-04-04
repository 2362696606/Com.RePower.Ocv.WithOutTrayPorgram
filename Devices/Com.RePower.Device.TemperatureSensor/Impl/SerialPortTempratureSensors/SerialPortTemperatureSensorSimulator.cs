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
            this.SerialPortDeviceBase = new SerialPortDeviceBaseSimulator();
            if(SerialPortDeviceBase is SerialPortDeviceBaseSimulator tempDeviceBase)
            {
                tempDeviceBase.RecoveryMethod = RecoveryMethod;
            }
        }

        private byte[] RecoveryMethod(byte[] arg)
        {
            return new byte[] { 0x01, 0x03, 0x10, 0x00, 0xDE, 0x00, 0xDE, 0x00, 0xDF, 0x01, 0x42, 0x18, 0x09, 0x18, 0x09, 0x21, 0x34, 0x21, 0x34, 0x66, 0xA9 };
        }
    }
}
