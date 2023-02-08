using Com.RePower.DeviceBase.BaseDevice;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A
{
    public class Keysight_34461AImpl : Keysight_34461AAbstract
    {
        protected INetDeviceBase netDeviceBase;
        public Keysight_34461AImpl()
        {
            this.netDeviceBase = new TcpNetDeviceBase();
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
            get { return netDeviceBase.ReadDelay;}
            set { netDeviceBase.ReadDelay = value;}
        }

        public override string IpAddress
        {
            get { return netDeviceBase.IpAddress;}
            set { netDeviceBase.IpAddress = value;}
        }
        public override int Port
        {
            get { return netDeviceBase.Port;}
            set { netDeviceBase.Port = value;}
        }

        public override OperateResult Connect()
        {
            return netDeviceBase.Connect();
        }

        public override OperateResult Connect(string ipAddress, int port)
        {
            return netDeviceBase.Connect(ipAddress, port);
        }

        public override async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await netDeviceBase.ConnectAsync(ipAddress, port);
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
        public override Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return netDeviceBase.SendCmdAsync(cmd, timeout, isNeedRecovery);
        }
    }
}
