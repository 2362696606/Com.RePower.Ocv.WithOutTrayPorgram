using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.Helper;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Ohm.Impl.Hioki_BT3562
{
    public class Hioki_BT3562Impl : Hioki_BT3562Abstruct, IOhmSerialPort
    {
        protected ISerialPortDeviceBase deviceBase;
        public Hioki_BT3562Impl()
        {
            this.deviceBase = new SerialPortDeviceBase();
        }
        public virtual int ReadDelay
        {
            get 
            {
                var dev = deviceBase as SerialPortDeviceBase;
                return dev?.ReadDelay ?? -1;
            }
            set
            {
                var dev = deviceBase as SerialPortDeviceBase;
                if (dev != null)
                {
                    dev.ReadDelay = value;
                }
            }
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
        /// <summary>
        /// 配置连续测量开关
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public OperateResult SetInitiateContinuous(bool isOpen = false)
        {
            string cmdStr = ":INITIATE:CONTINUOUS " + (isOpen?"ON":"OFF")+ "\r\n";
            byte[] cmd = Encoding.ASCII.GetBytes(cmdStr);
            var result = SendCmd(cmd.ToArray(), isNeedRecovery: false);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult<double>($"读取内阻失败:{result.Message ?? "未知原因"}");
            }
            return OperateResult.CreateSuccessResult();
        }
        public OperateResult SetRang(string range = "3.0000E-3")
        {
            string cmdStr = ":RES:RANG " + range + "\r\n";
            byte[] cmd = Encoding.ASCII.GetBytes(cmdStr);
            var result = SendCmd(cmd.ToArray(), isNeedRecovery: false);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult<double>($"读取内阻失败:{result.Message ?? "未知原因"}");
            }
            return OperateResult.CreateSuccessResult();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return deviceBase.SendCmd(cmd,timeout,isNeedRecovery);
        }
    }
}
