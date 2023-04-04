using Com.RePower.Device.Plc.Extensions;
using Com.RePower.Device.Plc.Helper;
using Com.RePower.DeviceBase.Plc;
using HslCommunication.Core.Net;
using HslCommunication.LogNet;
using System.Text;

namespace Com.RePower.Device.Plc
{
    public abstract class PlcNetAbstract : IPlcNet
    {
        protected NetworkDeviceBase NetWorkDeviceBase = new();

        protected PlcNetAbstract()
        {
            this.NetWorkDeviceBase.LogNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/PlcLogs"), GenerateMode.ByEveryDay, 30);
            this.NetWorkDeviceBase.SetPersistentConnection();
        }

        protected PlcNetAbstract(NetworkDeviceBase networkDeviceBase)
        {
            this.NetWorkDeviceBase = networkDeviceBase;
            this.NetWorkDeviceBase.LogNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/PlcLogs"), GenerateMode.ByEveryDay, 30);
            this.NetWorkDeviceBase.SetPersistentConnection();
        }

        protected virtual void OnDeviceCreated()
        {
        }

        public string IpAddress
        {
            get => this.NetWorkDeviceBase.IpAddress;
            set => this.NetWorkDeviceBase.IpAddress = value;
        }

        public int Port
        {
            get => this.NetWorkDeviceBase.Port;
            set => this.NetWorkDeviceBase.Port = value;
        }

        public string DeviceName { get; set; } = "UnnamedDevice";

        public bool IsConnected { get; set; } = false;

        public WpfBase.OperateResult Connect(string ipAddress, int port)
        {
            NetWorkDeviceBase.IpAddress = ipAddress;
            NetWorkDeviceBase.Port = port;
            var result = NetWorkDeviceBase.ConnectServer();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Connect()
        {
            var result = NetWorkDeviceBase.ConnectServer();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> ConnectAsync()
        {
            var result = await NetWorkDeviceBase.ConnectServerAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> ConnectAsync(string ipAddress, int port)
        {
            NetWorkDeviceBase.IpAddress = ipAddress;
            NetWorkDeviceBase.Port = port;
            var result = await NetWorkDeviceBase.ConnectServerAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult DisConnect()
        {
            var result = NetWorkDeviceBase.ConnectClose();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> DisConnectAsync()
        {
            var result = await NetWorkDeviceBase.ConnectCloseAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public void Dispose()
        {
            NetWorkDeviceBase.Dispose();
        }

        public WpfBase.OperateResult<byte[]> Read(string address, ushort length)
        {
            var result = NetWorkDeviceBase.Read(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadBool(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<bool> ReadBool(string address)
        {
            var result = NetWorkDeviceBase.ReadBool(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadBoolAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<bool>> ReadBoolAsync(string address)
        {
            var result = await NetWorkDeviceBase.ReadBoolAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<double> ReadDouble(string address)
        {
            var result = NetWorkDeviceBase.ReadDouble(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadDouble(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<double>> ReadDoubleAsync(string address)
        {
            var result = await NetWorkDeviceBase.ReadDoubleAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<double[]>> ReadDoubleAsync(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadDoubleAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<float> ReadFloat(string address)
        {
            var result = NetWorkDeviceBase.ReadFloat(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadFloat(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<float>> ReadFloatAsync(string address)
        {
            var result = await NetWorkDeviceBase.ReadFloatAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadFloatAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<short> ReadInt16(string address)
        {
            var result = NetWorkDeviceBase.ReadInt16(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadInt16(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<short>> ReadInt16Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadInt16Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadInt16Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<int> ReadInt32(string address)
        {
            var result = NetWorkDeviceBase.ReadInt32(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadInt32(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<int>> ReadInt32Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadInt32Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<int[]>> ReadInt32Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadInt32Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<long> ReadInt64(string address)
        {
            var result = NetWorkDeviceBase.ReadInt64(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadInt64(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<long>> ReadInt64Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadInt64Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<long[]>> ReadInt64Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadInt64Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadString(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            var result = NetWorkDeviceBase.ReadString(address, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadStringAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding)
        {
            var result = await NetWorkDeviceBase.ReadStringAsync(address, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ushort> ReadUInt16(string address)
        {
            var result = NetWorkDeviceBase.ReadUInt16(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadUInt16(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ushort>> ReadUInt16Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadUInt16Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadUInt16Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<uint> ReadUInt32(string address)
        {
            var result = NetWorkDeviceBase.ReadUInt32(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadUInt32(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<uint>> ReadUInt32Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadUInt32Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<uint[]>> ReadUInt32Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadUInt32Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ulong> ReadUInt64(string address)
        {
            var result = NetWorkDeviceBase.ReadUInt64(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            var result = NetWorkDeviceBase.ReadUInt64(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ulong>> ReadUInt64Async(string address)
        {
            var result = await NetWorkDeviceBase.ReadUInt64Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length)
        {
            var result = await NetWorkDeviceBase.ReadUInt64Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address,waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address,waitValue,readInterval,waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address,waitValue,readInterval,waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public WpfBase.OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address,waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address,waitValue,readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        //public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        //{
        //    var result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
        //    ChangeConnectedStatus(result);
        //    return result.DoConvert();
        //}

        public WpfBase.OperateResult Write(string address, byte[] value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, bool[] value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, bool value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, short value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, short[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ushort value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ushort[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, int value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, int[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, uint value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, uint[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, long value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, long[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ulong value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ulong[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, float value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, float[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, double value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, double[] values)
        {
            var result = NetWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value)
        {
            var result = NetWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, Encoding encoding)
        {
            var result = NetWorkDeviceBase.Write(address, value, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, int length)
        {
            var result = NetWorkDeviceBase.Write(address, value, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            var result = NetWorkDeviceBase.Write(address, value, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, byte[] value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, bool[] value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, bool value)
        {
            var result = await NetWorkDeviceBase.WaitAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, short value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, short[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ushort value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ushort[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, int value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, int[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, uint value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, uint[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, long value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, long[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ulong value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ulong[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, float value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, float[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, double value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, double[] values)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, Encoding encoding)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, int length)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, int length, Encoding encoding)
        {
            var result = await NetWorkDeviceBase.WriteAsync(address, value, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        protected virtual void ChangeConnectedStatus(HslCommunication.OperateResult result)
        {
            if (result.ErrorCode < 0)
            {
                IsConnected = false;
            }
            else
            {
                IsConnected = true;
            }
        }

        protected virtual void ChangeConnectedStatus<T>(HslCommunication.OperateResult<T> result)
        {
            if (result.ErrorCode < 0)
            {
                IsConnected = false;
            }
            else
            {
                IsConnected = true;
            }
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = NetWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await NetWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }
    }
}