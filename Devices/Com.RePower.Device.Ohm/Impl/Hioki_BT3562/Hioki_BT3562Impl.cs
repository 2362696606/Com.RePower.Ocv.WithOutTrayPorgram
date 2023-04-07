using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.WpfBase;
using System.Text;

namespace Com.RePower.Device.Ohm.Impl.Hioki_BT3562
{
    public class HiokiBt3562Impl : HiokiBt3562Abstruct, IOhmSerialPort
    {
        protected ISerialPortDeviceBase DeviceBase;

        public HiokiBt3562Impl()
        {
            this.DeviceBase = new SerialPortDeviceBase();
        }

        public virtual int ReadDelay
        {
            get
            {
                var dev = DeviceBase as SerialPortDeviceBase;
                return dev?.ReadDelay ?? -1;
            }
            set
            {
                var dev = DeviceBase as SerialPortDeviceBase;
                if (dev != null)
                {
                    dev.ReadDelay = value;
                }
            }
        }

        public string PortName
        {
            get { return DeviceBase.PortName; }
            set { DeviceBase.PortName = value; }
        }

        public int BaudRate
        {
            get { return DeviceBase.BaudRate; }
            set { DeviceBase.BaudRate = value; }
        }

        public override bool IsConnected
        {
            get { return DeviceBase.IsConnected; }
        }

        public override string DeviceName
        {
            get { return DeviceBase.DeviceName; }
            set { DeviceBase.DeviceName = value; }
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            return DeviceBase.Connect(portName, baudRate);
        }

        public override OperateResult Connect()
        {
            return DeviceBase.Connect();
        }

        public Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            return DeviceBase.ConnectAsync(portName, baudRate);
        }

        public override OperateResult DisConnect()
        {
            return DeviceBase.DisConnect();
        }

        public override void Dispose()
        {
            DeviceBase.Dispose();
        }

        /// <summary>
        /// 配置连续测量开关
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public OperateResult SetInitiateContinuous(bool isOpen = false)
        {
            string cmdStr = ":INITIATE:CONTINUOUS " + (isOpen ? "ON" : "OFF") + "\r\n";
            byte[] cmd = Encoding.ASCII.GetBytes(cmdStr);
            var result = SendCmd(cmd.ToArray(), isNeedRecovery: false);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult<double>(isOpen ? "开启" : "关闭" + $"连续测量失败:{result.Message ?? "未知原因"}");
            }
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult SetRang(string range = "30.000E-3")
        {
            string cmdStr = ":RES:RANG " + range + "\r\n";
            byte[] cmd = Encoding.ASCII.GetBytes(cmdStr);
            var result = SendCmd(cmd.ToArray(), isNeedRecovery: false);
            if (result.IsFailed)
            {
                return OperateResult.CreateFailedResult<double>($"设置量程失败:{result.Message ?? "未知原因"}");
            }
            return OperateResult.CreateSuccessResult();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return DeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}