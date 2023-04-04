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
        protected ISerialPortDeviceBase SerialPortDeviceBase;
        public SerialPortTemperatureSensorImpl()
        {
            this.SerialPortDeviceBase = new SerialPortDeviceBase();
        }
        public override string PortName { get => SerialPortDeviceBase.PortName; set => SerialPortDeviceBase.PortName = value; }
        public override int BaudRate { get => SerialPortDeviceBase.BaudRate; set => SerialPortDeviceBase.BaudRate = value; }

        public override bool IsConnected => SerialPortDeviceBase.IsConnected;

        public override string DeviceName { get => SerialPortDeviceBase.DeviceName; set => SerialPortDeviceBase.DeviceName = value; }
        public override int ReadDelay { get => SerialPortDeviceBase.ReadDelay; set => SerialPortDeviceBase.ReadDelay = value; }

        public override OperateResult Connect(string portName, int baudRate)
        {
            return SerialPortDeviceBase.Connect(portName, baudRate);
        }
        
        public override OperateResult Connect()
        {
            return SerialPortDeviceBase.Connect();
        }

        public override OperateResult DisConnect()
        {
            return SerialPortDeviceBase.DisConnect();
        }

        public override void Dispose()
        {
            SerialPortDeviceBase.Dispose();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return SerialPortDeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}
