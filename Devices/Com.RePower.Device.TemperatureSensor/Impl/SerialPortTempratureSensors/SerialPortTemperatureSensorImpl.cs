using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor.Impl.SerialPortTempratureSensors
{
    public class SerialPortTemperatureSensorImpl : SerialPortTemperatureSensorAbstruct
    {
        protected ISerialPortDeviceBase serialPortDeviceBase;
        public SerialPortTemperatureSensorImpl()
        {
            this.serialPortDeviceBase = new SerialPortDeviceBase();
        }
        public override string PortName { get => serialPortDeviceBase.PortName; set => serialPortDeviceBase.PortName = value; }
        public override int BaudRate { get => serialPortDeviceBase.BaudRate; set => serialPortDeviceBase.BaudRate = value; }

        public override bool IsConnected => serialPortDeviceBase.IsConnected;

        public override string DeviceName { get => serialPortDeviceBase.DeviceName; set => serialPortDeviceBase.DeviceName = value; }
        public override int ReadDelsy { get => serialPortDeviceBase.ReadDelay; set => serialPortDeviceBase.ReadDelay = value; }

        public override OperateResult Connect(string portName, int baudRate)
        {
            return serialPortDeviceBase.Connect(portName, baudRate);
        }

        public override OperateResult Connect()
        {
            return serialPortDeviceBase.Connect();
        }

        public override OperateResult DisConnect()
        {
            return serialPortDeviceBase.DisConnect();
        }

        public override void Dispose()
        {
            serialPortDeviceBase.Dispose();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return serialPortDeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}
