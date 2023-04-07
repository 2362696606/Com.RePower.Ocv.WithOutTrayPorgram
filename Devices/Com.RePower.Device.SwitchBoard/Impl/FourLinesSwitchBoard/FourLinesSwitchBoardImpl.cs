using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;

namespace Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard
{
    public class FourLinesSwitchBoardImpl : FourLinesSwitchBoardAbstract, ISwitchBoardSerialPort
    {
        protected ISerialPortDeviceBase DeviceBase;

        public FourLinesSwitchBoardImpl()
        {
            DeviceBase = new SerialPortDeviceBase();
        }

        public override bool IsConnected
        { get { return DeviceBase.IsConnected; } }

        public override string DeviceName
        {
            get { return DeviceBase.DeviceName; }
            set { DeviceBase.DeviceName = value; }
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

        /// <summary>
        /// 读取延迟
        /// </summary>
        public override int ReadDelay
        {
            get { return DeviceBase.ReadDelay; }
            set { DeviceBase.ReadDelay = value; }
        }

        public override OperateResult Connect()
        {
            return DeviceBase.Connect();
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            return DeviceBase.Connect(portName, baudRate);
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

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return DeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}