using Autofac.Features.Decorators;
using Com.RePower.DeviceBase.Extensions;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Decorators
{
    public class PlcDecorator : IPlc
    {
        private readonly IPlc plc;
        private readonly IDecoratorContext context;

        public PlcDecorator(IPlc plc, IDecoratorContext context)
        {
            this.plc = plc ?? throw new ArgumentNullException(nameof(plc));
            this.context = context ?? throw new ArgumentNullException(nameof(plc));
        }

        public bool IsConnected
        {
            get { return plc.IsConnected; }
        }

        public string DeviceName
        {
            get { return plc.DeviceName; }
            set { plc.DeviceName = value; }
        }

        public WpfBase.OperateResult Connect()
        {
            LogHelper.UiLog.Debug("连接Plc");
            return plc.Connect();
        }

        public Task<WpfBase.OperateResult> ConnectAsync()
        {
            LogHelper.UiLog.Debug("异步连接Plc");
            return plc.ConnectAsync();
        }

        public WpfBase.OperateResult DisConnect()
        {
            LogHelper.UiLog.Debug("Plc断开连接");
            return plc.DisConnect();
        }

        public Task<WpfBase.OperateResult> DisConnectAsync()
        {
            LogHelper.UiLog.Debug("Plc异步断开连接");
            return plc.DisConnectAsync();
        }

        public void Dispose()
        {
            plc.Dispose();
        }

        public WpfBase.OperateResult<byte[]> Read(string address, ushort length)
        {
            var result =  plc.Read(address, length);
            LogHelper.UiLog.Debug($"读取byte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            var result = await plc.ReadAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取bte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            var result = plc.ReadBool(address, length);
            LogHelper.UiLog.Debug($"读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<bool> ReadBool(string address)
        {
            var result = plc.ReadBool(address);
            LogHelper.UiLog.Debug($"读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            var result = await plc.ReadBoolAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<bool>> ReadBoolAsync(string address)
        {
            var result = await plc.ReadBoolAsync(address);
            LogHelper.UiLog.Debug($"异步读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<double> ReadDouble(string address)
        {
            var result = plc.ReadDouble(address);
            LogHelper.UiLog.Debug($"读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            var result = plc.ReadDouble(address,length);
            LogHelper.UiLog.Debug($"读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<double>> ReadDoubleAsync(string address)
        {
            var result = await plc.ReadDoubleAsync(address);
            LogHelper.UiLog.Debug($"异步读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<double[]>> ReadDoubleAsync(string address, ushort length)
        {
            var result = await plc.ReadDoubleAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<float> ReadFloat(string address)
        {
            var result = plc.ReadFloat(address);
            LogHelper.UiLog.Debug($"读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            var result = plc.ReadFloat(address, length);
            LogHelper.UiLog.Debug($"读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<float>> ReadFloatAsync(string address)
        {
            var result = await plc.ReadFloatAsync(address);
            LogHelper.UiLog.Debug($"异步读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            var result = await plc.ReadFloatAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<short> ReadInt16(string address)
        {
            var result = plc.ReadInt16(address);
            LogHelper.UiLog.Debug($"读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            var result = plc.ReadInt16(address, length);
            LogHelper.UiLog.Debug($"读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<short>> ReadInt16Async(string address)
        {
            var result = await plc.ReadInt16Async(address);
            LogHelper.UiLog.Debug($"异步读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            var result = await plc.ReadInt16Async(address, length);
            LogHelper.UiLog.Debug($"异步读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<int> ReadInt32(string address)
        {
            var result = plc.ReadInt32(address);
            LogHelper.UiLog.Debug($"读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            var result = plc.ReadInt32(address, length);
            LogHelper.UiLog.Debug($"读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<int>> ReadInt32Async(string address)
        {
            var result = await plc.ReadInt32Async(address);
            LogHelper.UiLog.Debug($"异步读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<int[]>> ReadInt32Async(string address, ushort length)
        {
            var result = await plc.ReadInt32Async(address, length);
            LogHelper.UiLog.Debug($"异步读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<long> ReadInt64(string address)
        {
            var result = plc.ReadInt64(address);
            LogHelper.UiLog.Debug($"读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            var result = plc.ReadInt64(address, length);
            LogHelper.UiLog.Debug($"读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<long>> ReadInt64Async(string address)
        {
            var result = await plc.ReadInt64Async(address);
            LogHelper.UiLog.Debug($"异步读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<long[]>> ReadInt64Async(string address, ushort length)
        {
            var result = await plc.ReadInt64Async(address, length);
            LogHelper.UiLog.Debug($"异步读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length)
        {
            var result = plc.ReadString(address, length);
            LogHelper.UiLog.Debug($"读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            var result = plc.ReadString(address, length);
            LogHelper.UiLog.Debug($"读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            var result = await plc.ReadStringAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding)
        {
            var result = await plc.ReadStringAsync(address, length);
            LogHelper.UiLog.Debug($"异步读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<ushort> ReadUInt16(string address)
        {
            var result = plc.ReadUInt16(address);
            LogHelper.UiLog.Debug($"读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            var result = plc.ReadUInt16(address, length);
            LogHelper.UiLog.Debug($"读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<ushort>> ReadUInt16Async(string address)
        {
            var result = await plc.ReadUInt16Async(address);
            LogHelper.UiLog.Debug($"异步读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length)
        {
            var result = await plc.ReadUInt16Async(address, length);
            LogHelper.UiLog.Debug($"异步读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<uint> ReadUInt32(string address)
        {
            var result = plc.ReadUInt32(address);
            LogHelper.UiLog.Debug($"读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            var result = plc.ReadUInt32(address, length);
            LogHelper.UiLog.Debug($"读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<uint>> ReadUInt32Async(string address)
        {
            var result = await plc.ReadUInt32Async(address);
            LogHelper.UiLog.Debug($"异步读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<uint[]>> ReadUInt32Async(string address, ushort length)
        {
            var result = await plc.ReadUInt32Async(address, length);
            LogHelper.UiLog.Debug($"异步读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<ulong> ReadUInt64(string address)
        {
            var result = plc.ReadUInt64(address);
            LogHelper.UiLog.Debug($"读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public WpfBase.OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            var result = plc.ReadUInt64(address, length);
            LogHelper.UiLog.Debug($"读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<WpfBase.OperateResult<ulong>> ReadUInt64Async(string address)
        {
            var result = await plc.ReadUInt64Async(address);
            LogHelper.UiLog.Debug($"异步读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<WpfBase.OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length)
        {
            var result = await plc.ReadUInt64Async(address, length);
            LogHelper.UiLog.Debug($"异步读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public WpfBase.OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = plc.Wait(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<WpfBase.OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult Write(string address, byte[] value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, bool[] value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, bool value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, short value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, short[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ushort value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ushort[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, int value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, int[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, uint value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, uint[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, long value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, long[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, ulong value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ulong[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, float value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, float[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, double value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, double[] values)
        {
            var result = plc.Write(address, values);
            LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, string value)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, Encoding encoding)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, int length)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            var result = plc.Write(address, value);
            LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, byte[] value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, bool[] value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, bool value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, short value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, short[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ushort value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ushort[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, int value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, int[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, uint value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, uint[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, long value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, long[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ulong value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ulong[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, float value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, float[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, double value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, double[] values)
        {
            var result = await plc.WriteAsync(address, values);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, Encoding encoding)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, int length)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, int length, Encoding encoding)
        {
            var result = await plc.WriteAsync(address, value);
            LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},长度{length},结果{result.IsSuccess}");
            return result;
        }
    }
}
