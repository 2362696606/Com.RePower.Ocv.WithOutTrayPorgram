using Com.RePower.Device.Plc.Extensions;
using Com.RePower.Device.Plc.Helper;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.WpfBase;
using HslCommunication;
using HslCommunication.Core;
using HslCommunication.Core.Net;
using HslCommunication.LogNet;
using HslCommunication.Profinet.Inovance;
using System.Text;

namespace Com.RePower.Device.Plc
{
    public abstract class PlcNetAbstract : IPlcNet
    {
        protected NetworkDeviceBase netWorkDeviceBase = new NetworkDeviceBase();

        protected bool _isConnected = false;

        protected string _deviceName = "UnnamedDevice";

        public PlcNetAbstract()
        {
            OnDeviceCreated();
        }
        public PlcNetAbstract(NetworkDeviceBase networkDeviceBase)
        {
            this.netWorkDeviceBase= networkDeviceBase;
            OnDeviceCreated();
        }

        protected virtual void OnDeviceCreated()
        {
            this.netWorkDeviceBase.LogNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/PlcLogs"), GenerateMode.ByEveryDay, 30);
            this.netWorkDeviceBase.SetPersistentConnection();
        }

        public string IpAddress
        {
            get { return this.netWorkDeviceBase.IpAddress; }
            set { this.netWorkDeviceBase.IpAddress = value; }
        }

        public int Port
        {
            get { return this.netWorkDeviceBase.Port; }
            set { this.netWorkDeviceBase.Port = value; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; }
        }

        public WpfBase.OperateResult Connect(string ipAddress, int port)
        {
            netWorkDeviceBase.IpAddress = ipAddress;
            netWorkDeviceBase.Port = port;
            var result = netWorkDeviceBase.ConnectServer();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Connect()
        {
            var result = netWorkDeviceBase.ConnectServer();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> ConnectAsync()
        {
            var result = await netWorkDeviceBase.ConnectServerAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> ConnectAsync(string ipAddress, int port)
        {
            netWorkDeviceBase.IpAddress = ipAddress;
            netWorkDeviceBase.Port = port;
            var result = await netWorkDeviceBase.ConnectServerAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult DisConnect()
        {
            var result = netWorkDeviceBase.ConnectClose();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> DisConnectAsync()
        {
            var result = await netWorkDeviceBase.ConnectCloseAsync();
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public void Dispose()
        {
            netWorkDeviceBase.Dispose();
        }

        public WpfBase.OperateResult<byte[]> Read(string address, ushort length)
        {
            var result = netWorkDeviceBase.Read(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadBool(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<bool> ReadBool(string address)
        {
            var result = netWorkDeviceBase.ReadBool(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadBoolAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<bool>> ReadBoolAsync(string address)
        {
            var result = await netWorkDeviceBase.ReadBoolAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<double> ReadDouble(string address)
        {
            var result = netWorkDeviceBase.ReadDouble(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadDouble(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<double>> ReadDoubleAsync(string address)
        {
            var result = await netWorkDeviceBase.ReadDoubleAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<double[]>> ReadDoubleAsync(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadDoubleAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<float> ReadFloat(string address)
        {
            var result = netWorkDeviceBase.ReadFloat(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadFloat(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<float>> ReadFloatAsync(string address)
        {
            var result = await netWorkDeviceBase.ReadFloatAsync(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadFloatAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<short> ReadInt16(string address)
        {
            var result = netWorkDeviceBase.ReadInt16(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadInt16(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<short>> ReadInt16Async(string address)
        {
            var result = await netWorkDeviceBase.ReadInt16Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadInt16Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<int> ReadInt32(string address)
        {
            var result = netWorkDeviceBase.ReadInt32(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadInt32(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<int>> ReadInt32Async(string address)
        {
            var result = await netWorkDeviceBase.ReadInt32Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<int[]>> ReadInt32Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadInt32Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<long> ReadInt64(string address)
        {
            var result = netWorkDeviceBase.ReadInt64(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadInt64(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<long>> ReadInt64Async(string address)
        {
            var result = await netWorkDeviceBase.ReadInt64Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<long[]>> ReadInt64Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadInt64Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadString(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            var result = netWorkDeviceBase.ReadString(address, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadStringAsync(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding)
        {
            var result = await netWorkDeviceBase.ReadStringAsync(address, length, encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ushort> ReadUInt16(string address)
        {
            var result = netWorkDeviceBase.ReadUInt16(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadUInt16(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ushort>> ReadUInt16Async(string address)
        {
            var result = await netWorkDeviceBase.ReadUInt16Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadUInt16Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<uint> ReadUInt32(string address)
        {
            var result = netWorkDeviceBase.ReadUInt32(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadUInt32(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<uint>> ReadUInt32Async(string address)
        {
            var result = await netWorkDeviceBase.ReadUInt32Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<uint[]>> ReadUInt32Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadUInt32Async(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ulong> ReadUInt64(string address)
        {
            var result = netWorkDeviceBase.ReadUInt64(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            var result = netWorkDeviceBase.ReadUInt64(address, length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ulong>> ReadUInt64Async(string address)
        {
            var result = await netWorkDeviceBase.ReadUInt64Async(address);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length)
        {
            var result = await netWorkDeviceBase.ReadUInt64Async(address, length);
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
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, bool[] value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, bool value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, short value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, short[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ushort value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ushort[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, int value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, int[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, uint value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, uint[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, long value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, long[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ulong value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, ulong[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, float value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, float[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, double value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, double[] values)
        {
            var result = netWorkDeviceBase.Write(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value)
        {
            var result = netWorkDeviceBase.Write(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, Encoding encoding)
        {
            var result = netWorkDeviceBase.Write(address,value,encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, int length)
        {
            var result = netWorkDeviceBase.Write(address,value,length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            var result = netWorkDeviceBase.Write(address,value,length,encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, byte[] value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, bool[] value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, bool value)
        {
            var result = await netWorkDeviceBase.WaitAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, short value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, short[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ushort value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ushort[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, int value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, int[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, uint value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, uint[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, long value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, long[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ulong value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, ulong[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, float value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, float[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, double value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, double[] values)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, values);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, Encoding encoding)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,value,encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, int length)
        {
            var result = await netWorkDeviceBase.WriteAsync(address, value,length);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult> WriteAsync(string address, string value, int length, Encoding encoding)
        {
            var result = await netWorkDeviceBase.WriteAsync(address,value,length,encoding);
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }
        protected virtual void ChangeConnectedStatus(HslCommunication.OperateResult result)
        {
            if(result.ErrorCode<0)
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
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = netWorkDeviceBase.Wait(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            HslCommunication.OperateResult<TimeSpan> result = new HslCommunication.OperateResult<TimeSpan>();
            if (cancellation is { } cancel)
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout, cancel);
            }
            else
            {
                result = await netWorkDeviceBase.WaitAsync(address, waitValue, readInterval, waitTimeout);
            }
            ChangeConnectedStatus(result);
            return result.DoConvert();
        }
    }
}