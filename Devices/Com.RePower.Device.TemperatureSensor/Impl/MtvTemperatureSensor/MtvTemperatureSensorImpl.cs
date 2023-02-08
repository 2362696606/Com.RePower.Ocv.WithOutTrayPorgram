using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor.Impl.MtvTemperatureSensor
{
    public class MtvTemperatureSensorImpl : MtvTemperatureSensorAbstruct
    {
        protected INetDeviceBase netDeviceBase;
        public MtvTemperatureSensorImpl()
        {
            this.netDeviceBase = new UdpNetDeviceBase();
        }

        public override string IpAddress
        {
            get { return netDeviceBase.IpAddress; }
            set { netDeviceBase.IpAddress = value; }
        }

        public override int Port
        {
            get { return netDeviceBase.Port; }
            set { netDeviceBase.Port = value; }
        }

        public override bool IsConnected
        {
            get { return netDeviceBase.IsConnected; }
        }

        public override string DeviceName
        {
            get { return netDeviceBase.DeviceName; }
            set { netDeviceBase.DeviceName = value; }
        }
        public override int ReadDelay
        {
            get { return netDeviceBase.ReadDelay; }
            set { netDeviceBase.ReadDelay = value; }
        }

        public override OperateResult Connect(string ipAddress, int port)
        {
            return netDeviceBase.Connect(ipAddress, port);
        }

        public override OperateResult Connect()
        {
            return netDeviceBase.Connect();
        }

        public override OperateResult DisConnect()
        {
            return netDeviceBase.DisConnect();
        }

        public override void Dispose()
        {
            netDeviceBase.Dispose();
        }

        public override OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return netDeviceBase.SendCmd(cmd, timeout, isNeedRecovery);
        }
    }
}
