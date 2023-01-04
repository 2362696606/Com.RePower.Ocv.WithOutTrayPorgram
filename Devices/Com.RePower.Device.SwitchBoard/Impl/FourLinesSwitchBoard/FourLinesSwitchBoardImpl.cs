using Autofac.Core.Resolving.Middleware;
using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard
{
    public class FourLinesSwitchBoardImpl : FourLinesSwitchBoardAbstract, ISwitchBoardSerialPort
    {
        protected ISerialPortDeviceBase deviceBase;

        public FourLinesSwitchBoardImpl()
        {
            deviceBase = new SerialPortDeviceBase();
        }

        public override bool IsConnected { get { return deviceBase.IsConnected; } }

        public override string DeviceName
        {
            get { return deviceBase.DeviceName; }
            set { deviceBase.DeviceName = value; }
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
        /// <summary>
        /// 读取延迟
        /// </summary>
        public override int ReadDelay
        {
            get { return deviceBase.ReadDelay;}
            set { deviceBase.ReadDelay = value;}
        }

        public override OperateResult Connect()
        {
            return deviceBase.Connect();
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            return deviceBase.Connect(portName, baudRate);
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
            return deviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}
