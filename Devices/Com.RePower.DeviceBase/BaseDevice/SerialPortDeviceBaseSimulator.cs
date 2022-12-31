﻿using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class SerialPortDeviceBaseSimulator : ISerialPortDeviceBase
    {
        private bool disposedValue;
        public SerialPortDeviceBaseSimulator()
        {
            Model = SerialPortSimulatorDeviceModel.SwitchBoard;
        }
        /// <summary>
        /// 模拟构造
        /// </summary>
        /// <param name="module">1为切换板，2为内阻仪</param>
        public SerialPortDeviceBaseSimulator(SerialPortSimulatorDeviceModel model)
        {
            Model = model;
        }

        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 5002;

        public bool IsConnected { get; set; }

        public string DeviceName { get; set; } = "UnnamedDevice";
        public SerialPortSimulatorDeviceModel Model { get; } = SerialPortSimulatorDeviceModel.SwitchBoard;
        public int ReadDelay { get; set; }

        public OperateResult Connect(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Connect()
        {
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            return await Task.Run(() => Connect(portName, baudRate));
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
                byte[] returnResult = cmd;
                if (Model == SerialPortSimulatorDeviceModel.Ohm)
                {
                    var random = new Random();
                    var randNum = random.NextDouble() * (0.001 - 0.0001) + 0.00001;

                    returnResult = Encoding.ASCII.GetBytes(randNum.ToString("f" + 6));
                }

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
        // ~SerialPortDeviceSimulatorBase()
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
