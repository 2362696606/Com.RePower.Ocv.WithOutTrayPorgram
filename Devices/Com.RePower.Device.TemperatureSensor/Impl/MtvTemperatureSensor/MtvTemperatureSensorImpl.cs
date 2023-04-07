using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.WpfBase;

namespace Com.RePower.Device.TemperatureSensor.Impl.MtvTemperatureSensor
{
    public class MtvTemperatureSensorImpl : MtvTemperatureSensorAbstruct
    {
        protected INetDeviceBase NetDeviceBase;

        public MtvTemperatureSensorImpl()
        {
            this.NetDeviceBase = new UdpNetDeviceBase();
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

        public override OperateResult Connect(string ipAddress, int port)
        {
            return NetDeviceBase.Connect(ipAddress, port);
        }

        public override OperateResult Connect()
        {
            return NetDeviceBase.Connect();
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
    }
}