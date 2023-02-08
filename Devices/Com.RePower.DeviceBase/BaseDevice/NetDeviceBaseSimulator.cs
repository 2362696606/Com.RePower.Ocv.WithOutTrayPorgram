using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class NetDeviceBaseSimulator : INetDeviceBase,ISimulatorRecovery
    {
        private bool disposedValue;

        public string IpAddress { get; set; } = "127.0.0.1";

        public int Port { get; set; } = 5020;

        public bool IsConnected { get; private set; }= false;

        public string DeviceName { get; set; } = "UnnamedDevice";

        public int ReadDelay { get; set ; }
        public Func<byte[], byte[]>? RecoveryMethod { get ; set; }

        public OperateResult Connect(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Connect()
        {
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await Task.Run(() => Connect(ipAddress,port));
        }

        public async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public OperateResult DisConnect()
        {
            this.IsConnected = false;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            Thread.Sleep(ReadDelay);
            if (isNeedRecovery)
            {
                var returnResult = RecoveryMethod?.Invoke(cmd) ?? cmd;
                return OperateResult.CreateSuccessResult(returnResult);
            }
            else
            {
                return OperateResult.CreateSuccessResult<byte[]>(null);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~NetDeviceBaseSimulator()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
