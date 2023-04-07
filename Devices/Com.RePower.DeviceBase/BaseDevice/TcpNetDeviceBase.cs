﻿using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase;
using System.Net.Sockets;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class TcpNetDeviceBase : INetDeviceBase
    {
        private TcpClient _tcpClient = new TcpClient();

        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            protected set { _tcpClient = value; }
        }

        private string _deviceName = "UnnamedDevice";

        private string _ipAddress = "127.0.0.1";

        private int _port = 502;

        private bool _disposedValue;

        public bool IsConnected
        {
            get
            {
                return TcpClient.Connected;
            }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public int ReadDelay { get; set; } = 1000;

        public OperateResult Connect()
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                TcpClient.Connect(IpAddress, Port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public OperateResult Connect(string ipAddress, int port)
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                this.IpAddress = ipAddress;
                this.Port = port;
                TcpClient.Connect(ipAddress, port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync()
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                await TcpClient.ConnectAsync(IpAddress, Port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                this.IpAddress = ipAddress;
                this.Port = port;
                await TcpClient.ConnectAsync(ipAddress, port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public OperateResult DisConnect()
        {
            try
            {
                this.TcpClient.Close();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    this.TcpClient.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                _disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~TcpNetDeviceBase()
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

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return TcpClient.SendAndRecovery(cmd, timeout, ReadDelay);
                }
                else
                {
                    TcpClient.GetStream().Write(cmd, 0, cmd.Length);
                    return OperateResult.CreateSuccessResult<byte[]>(null);
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return await TcpClient.SendAndRecoveryAsync(cmd, timeout, ReadDelay);
                }
                else
                {
                    await TcpClient.GetStream().WriteAsync(cmd, 0, cmd.Length);
                    return OperateResult.CreateSuccessResult<byte[]>(null);
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }
    }
}