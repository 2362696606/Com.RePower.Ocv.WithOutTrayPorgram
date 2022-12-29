using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Device.Ohm.Impl.Hioki_BT3562
{
    public class Hioki_BT3562Impl : Hioki_BT3562Abstruct, IOhmSerialPort
    {
        protected ISerialPortDeviceBase deviceBase;
        public Hioki_BT3562Impl()
        {
            this.deviceBase = new SerialPortDeviceBase();
        }
        public string PortName
        {
            get { return deviceBase.PortName; }
            set { deviceBase.PortName = value; }
        }
        public int BaudRate
        {
            get { return deviceBase.BaudRate; }
            set { deviceBase.BaudRate = value; }
        }

        public override bool IsConnected
        {
            get { return deviceBase.IsConnected; }
        }

        public override string DeviceName
        {
            get { return deviceBase.DeviceName; }
            set { deviceBase.DeviceName = value; }
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            return deviceBase.Connect(portName,baudRate);
        }

        public override OperateResult Connect()
        {
            return deviceBase.Connect();
        }

        public Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            return deviceBase.ConnectAsync(portName, baudRate);
        }

        public override OperateResult DisConnect()
        {
            return deviceBase.DisConnect();
        }

        public override void Dispose()
        {
            deviceBase.Dispose();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return deviceBase.SendCmd(cmd,timeout,isNeedRecovery);
        }
    }
}
