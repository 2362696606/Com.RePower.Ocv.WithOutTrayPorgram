using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM
{
    public abstract class DmmBase : IDmm
    {
        public abstract bool IsConnected { get; }

        public abstract string DeviceName { get; set; }

        public abstract OperateResult Connect();

        public virtual async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public abstract OperateResult DisConnect();

        public virtual async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        public abstract void Dispose();

        public abstract OperateResult<double> ReadAc();

        public virtual async Task<OperateResult<double>> ReadAcAsync()
        {
            return await Task.Run(() => ReadAc());
        }

        public abstract OperateResult<double> ReadDc();

        public virtual async Task<OperateResult<double>> ReadDcAsync()
        {
            return await Task.Run(() => ReadDc());
        }

        public abstract OperateResult<double> ReadRes();

        public virtual async Task<OperateResult<double>> ReadResAsync()
        {
            return await Task.Run(() => ReadRes());
        }

        public abstract OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        public virtual Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }
    }
}
