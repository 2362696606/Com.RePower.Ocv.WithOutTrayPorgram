using Com.RePower.DeviceBase.Ohm;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Ohm
{
    public abstract class OhmBase : IOhm
    {
        public abstract bool IsConnected { get; }

        public abstract string DeviceName { get; set; }

        public abstract OperateResult Connect();

        public async Task<OperateResult> ConnectAsync()
        {
            var result = await Task.Run<OperateResult>(() =>
            {
                return Connect();
            });
            return result;
        }

        public abstract OperateResult DisConnect();

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run<OperateResult>(() =>
            {
                return DisConnect();
            });
        }

        public abstract void Dispose();

        public abstract OperateResult<double> ReadRes();

        public async Task<OperateResult<double>> ReadResAsync()
        {
            return await Task.Run<OperateResult<double>>(() =>
            {
                return ReadRes();
            });
        }

        public abstract OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() =>
            {
                return SendCmd(cmd, timeout, isNeedRecovery);
            });
        }
    }
}
