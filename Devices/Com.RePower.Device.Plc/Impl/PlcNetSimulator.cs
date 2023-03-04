using Com.RePower.DeviceBase.Plc;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Impl
{
    public class PlcNetSimulator : IPlcNet
    {
        public string IpAddress { get; set; } = "127.0.0.1";
        public int Port { get ; set ; }

        public bool IsConnected
        {
            get { return true; }
        }

        public string DeviceName { get; set; } = "UnnamedDevice";

        public OperateResult Connect(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Connect()
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await Task.Run<OperateResult>(() => { return Connect(ipAddress, port); });
        }

        public async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run<OperateResult>(() => { return Connect(); });
        }

        public OperateResult DisConnect()
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run<OperateResult>(() => { return DisConnect(); });
        }

        public void Dispose()
        {
            
        }

        public OperateResult<byte[]> Read(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<bool> ReadBool(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<bool>> ReadBoolAsync(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<double> ReadDouble(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<double>> ReadDoubleAsync(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<double[]>> ReadDoubleAsync(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<float> ReadFloat(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<float>> ReadFloatAsync(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<short> ReadInt16(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<short>> ReadInt16Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<int> ReadInt32(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<int>> ReadInt32Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<int[]>> ReadInt32Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<long> ReadInt64(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<long>> ReadInt64Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<long[]>> ReadInt64Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> ReadString(string address, ushort length)
        {
            string randomNumStr = string.Format("{0:D5}", new Random().Next(10000));
            return OperateResult.CreateSuccessResult<string>($"TrayCode_{randomNumStr}");
            //return OperateResult.CreateSuccessResult<string>("TRAY_1234__360936453");
        }

        public OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            //return OperateResult.CreateSuccessResult<string>("TRAY_1234__360936453");
            string randomNumStr = string.Format("{0:D5}", new Random().Next(10000));
            return OperateResult.CreateSuccessResult<string>($"TrayCode_{randomNumStr}");
        }

        public Task<OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public OperateResult<ushort> ReadUInt16(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<ushort>> ReadUInt16Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<uint> ReadUInt32(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<uint>> ReadUInt32Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<uint[]>> ReadUInt32Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public OperateResult<ulong> ReadUInt64(string address)
        {
            throw new NotImplementedException();
        }

        public OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<ulong>> ReadUInt64Async(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length)
        {
            throw new NotImplementedException();
        }

        //public OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        //public OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    return OperateResult.CreateSuccessResult(new TimeSpan(10));
        //}

        public OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        public OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            return OperateResult.CreateSuccessResult(new TimeSpan(10));
        }

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            throw new NotImplementedException();
        }

        public OperateResult Write(string address, byte[] value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, bool[] value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, bool value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, short value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, short[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, ushort value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, ushort[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, int value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, int[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, uint value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, uint[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, long value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, long[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, ulong value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, ulong[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, float value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, float[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, double value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, double[] values)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, string value)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, string value, Encoding encoding)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, string value, int length)
        {
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            return OperateResult.CreateSuccessResult();
        }

        public Task<OperateResult> WriteAsync(string address, byte[] value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, bool[] value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, bool value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, short value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, short[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, ushort value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, ushort[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, int value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, int[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, uint value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, uint[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, long value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, long[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, ulong value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, ulong[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, float value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, float[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, double value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, double[] values)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, string value)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, string value, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, string value, int length)
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult> WriteAsync(string address, string value, int length, Encoding encoding)
        {
            throw new NotImplementedException();
        }
    }
}
