using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.WpfBase;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public class Keysight34461AImpl : Keysight34461AAbstract
    {
        protected INetDeviceBase NetDeviceBase;

        public Keysight34461AImpl()
        {
            this.NetDeviceBase = new TcpNetDeviceBase();
        }

        public override bool IsConnected
        {
            get { return NetDeviceBase.IsConnected; }
        }

        public override string DeviceName
        {
            get { return NetDeviceBase.DeviceName; }
            set { NetDeviceBase.DeviceName = value; }
        }

        public override int ReadDelay
        {
            get { return NetDeviceBase.ReadDelay; }
            set { NetDeviceBase.ReadDelay = value; }
        }

        public override string IpAddress
        {
            get { return NetDeviceBase.IpAddress; }
            set { NetDeviceBase.IpAddress = value; }
        }

        public override int Port
        {
            get { return NetDeviceBase.Port; }
            set { NetDeviceBase.Port = value; }
        }

        public override OperateResult Connect()
        {
            return NetDeviceBase.Connect();
        }

        public override OperateResult Connect(string ipAddress, int port)
        {
            return NetDeviceBase.Connect(ipAddress, port);
        }

        public override async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await NetDeviceBase.ConnectAsync(ipAddress, port);
        }

        public override OperateResult DisConnect()
        {
            return NetDeviceBase.DisConnect();
        }

        public override void Dispose()
        {
            NetDeviceBase.Dispose();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return NetDeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }

        public override Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return NetDeviceBase.SendCmdAsync(cmd, timeout, isNeedRecovery);
        }
    }
}