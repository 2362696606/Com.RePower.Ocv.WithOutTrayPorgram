using Com.RePower.DeviceBase.TemperatureSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.TemperatureSensor
{
    public abstract class TemperatureSensorBase : ITemperatureSensor
    {
        public abstract bool IsConnected { get; }

        public abstract string DeviceName { get; set; }

        public abstract RePower.WpfBase.OperateResult Connect();

        public virtual async Task<RePower.WpfBase.OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public abstract RePower.WpfBase.OperateResult DisConnect();

        public virtual async Task<RePower.WpfBase.OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        public abstract void Dispose();

        public abstract RePower.WpfBase.OperateResult<double[]> ReadTemp();

        public virtual async Task<RePower.WpfBase.OperateResult<double[]>> ReadTempAsync()
        {
            return await Task.Run(() => ReadTemp());
        }

        public abstract RePower.WpfBase.OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        public Task<RePower.WpfBase.OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }
    }
}
