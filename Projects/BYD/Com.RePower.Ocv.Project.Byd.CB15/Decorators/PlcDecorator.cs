using Autofac.Features.Decorators;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.WpfBase.Extensions;
using Com.RePower.WpfBase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Decorators
{
    public class PlcDecorator : IPlc
    {
        private readonly IPlc _plc;
        private readonly IDecoratorContext _context;

        public PlcDecorator(IPlc plc, IDecoratorContext context)
        {
            this._plc = plc ?? throw new ArgumentNullException(nameof(plc));
            this._context = context ?? throw new ArgumentNullException(nameof(plc));
        }

        public bool IsConnected
        {
            get { return _plc.IsConnected; }
        }

        public string DeviceName
        {
            get { return _plc.DeviceName; }
            set { _plc.DeviceName = value; }
        }

        public OperateResult Connect()
        {
            //LogHelper.UiLog.Debug("连接Plc");
            LogHelper.PlcLog.Debug("连接Plc");
            return _plc.Connect();
        }

        public Task<OperateResult> ConnectAsync()
        {
            //LogHelper.UiLog.Debug("异步连接Plc");
            LogHelper.PlcLog.Debug("异步连接Plc");
            return _plc.ConnectAsync();
        }

        public OperateResult DisConnect()
        {
            //LogHelper.UiLog.Debug("Plc断开连接");
            LogHelper.PlcLog.Debug("Plc断开连接");
            return _plc.DisConnect();
        }

        public Task<OperateResult> DisConnectAsync()
        {
            //LogHelper.UiLog.Debug("Plc异步断开连接");
            LogHelper.PlcLog.Debug("Plc异步断开连接");
            return _plc.DisConnectAsync();
        }

        public void Dispose()
        {
            _plc.Dispose();
        }

        public OperateResult<byte[]> Read(string address, ushort length)
        {
            var result = _plc.Read(address, length);
            //LogHelper.UiLog.Debug($"读取byte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取byte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            var result = await _plc.ReadAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取bte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取bte[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            var result = _plc.ReadBool(address, length);
            //LogHelper.UiLog.Debug($"读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<bool> ReadBool(string address)
        {
            var result = _plc.ReadBool(address);
            //LogHelper.UiLog.Debug($"读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            var result = await _plc.ReadBoolAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取bool[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<bool>> ReadBoolAsync(string address)
        {
            var result = await _plc.ReadBoolAsync(address);
            //LogHelper.UiLog.Debug($"异步读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取bool,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<double> ReadDouble(string address)
        {
            var result = _plc.ReadDouble(address);
            //LogHelper.UiLog.Debug($"读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            var result = _plc.ReadDouble(address, length);
            //LogHelper.UiLog.Debug($"读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<double>> ReadDoubleAsync(string address)
        {
            var result = await _plc.ReadDoubleAsync(address);
            //LogHelper.UiLog.Debug($"异步读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取double,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<double[]>> ReadDoubleAsync(string address, ushort length)
        {
            var result = await _plc.ReadDoubleAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取double[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<float> ReadFloat(string address)
        {
            var result = _plc.ReadFloat(address);
            //LogHelper.UiLog.Debug($"读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            var result = _plc.ReadFloat(address, length);
            //LogHelper.UiLog.Debug($"读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<float>> ReadFloatAsync(string address)
        {
            var result = await _plc.ReadFloatAsync(address);
            //LogHelper.UiLog.Debug($"异步读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取float,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            var result = await _plc.ReadFloatAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取float[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<short> ReadInt16(string address)
        {
            var result = _plc.ReadInt16(address);
            //LogHelper.UiLog.Debug($"读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            var result = _plc.ReadInt16(address, length);
            //LogHelper.UiLog.Debug($"读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<short>> ReadInt16Async(string address)
        {
            var result = await _plc.ReadInt16Async(address);
            //LogHelper.UiLog.Debug($"异步读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取int16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            var result = await _plc.ReadInt16Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取int16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<int> ReadInt32(string address)
        {
            var result = _plc.ReadInt32(address);
            //LogHelper.UiLog.Debug($"读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            var result = _plc.ReadInt32(address, length);
            //LogHelper.UiLog.Debug($"读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<int>> ReadInt32Async(string address)
        {
            var result = await _plc.ReadInt32Async(address);
            //LogHelper.UiLog.Debug($"异步读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取int32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<int[]>> ReadInt32Async(string address, ushort length)
        {
            var result = await _plc.ReadInt32Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取int32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<long> ReadInt64(string address)
        {
            var result = _plc.ReadInt64(address);
            //LogHelper.UiLog.Debug($"读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            var result = _plc.ReadInt64(address, length);
            //LogHelper.UiLog.Debug($"读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<long>> ReadInt64Async(string address)
        {
            var result = await _plc.ReadInt64Async(address);
            //LogHelper.UiLog.Debug($"异步读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取int64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<long[]>> ReadInt64Async(string address, ushort length)
        {
            var result = await _plc.ReadInt64Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取int64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<string> ReadString(string address, ushort length)
        {
            var result = _plc.ReadString(address, length);
            //LogHelper.UiLog.Debug($"读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            var result = _plc.ReadString(address, length);
            //LogHelper.UiLog.Debug($"读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            var result = await _plc.ReadStringAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取string,地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding)
        {
            var result = await _plc.ReadStringAsync(address, length);
            //LogHelper.UiLog.Debug($"异步读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取string,地址为{address},长度为{length},编码格式为{encoding},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<ushort> ReadUInt16(string address)
        {
            var result = _plc.ReadUInt16(address);
            //LogHelper.UiLog.Debug($"读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            var result = _plc.ReadUInt16(address, length);
            //LogHelper.UiLog.Debug($"读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<ushort>> ReadUInt16Async(string address)
        {
            var result = await _plc.ReadUInt16Async(address);
            //LogHelper.UiLog.Debug($"异步读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取uint16,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length)
        {
            var result = await _plc.ReadUInt16Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取uint16[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<uint> ReadUInt32(string address)
        {
            var result = _plc.ReadUInt32(address);
            //LogHelper.UiLog.Debug($"读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            var result = _plc.ReadUInt32(address, length);
            //LogHelper.UiLog.Debug($"读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<uint>> ReadUInt32Async(string address)
        {
            var result = await _plc.ReadUInt32Async(address);
            //LogHelper.UiLog.Debug($"异步读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取uint32,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<uint[]>> ReadUInt32Async(string address, ushort length)
        {
            var result = await _plc.ReadUInt32Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取uint32[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public OperateResult<ulong> ReadUInt64(string address)
        {
            var result = _plc.ReadUInt64(address);
            //LogHelper.UiLog.Debug($"读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            var result = _plc.ReadUInt64(address, length);
            //LogHelper.UiLog.Debug($"读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        public async Task<OperateResult<ulong>> ReadUInt64Async(string address)
        {
            var result = await _plc.ReadUInt64Async(address);
            //LogHelper.UiLog.Debug($"异步读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            LogHelper.PlcLog.Debug($"异步读取uint64,地址为{address},结果{result.IsSuccess},内容{result.Content}");
            return result;
        }

        public async Task<OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length)
        {
            var result = await _plc.ReadUInt64Async(address, length);
            //LogHelper.UiLog.Debug($"异步读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            LogHelper.PlcLog.Debug($"异步读取uint64[],地址为{address},长度为{length},结果{result.IsSuccess},内容{result.Content?.ToArrayString()}");
            return result;
        }

        [Obsolete("使用 Wait(string,bool,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,short,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,ushort,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,int,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,uint,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,long,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 Wait(string,ulong,int,int,CancellationToken?)", true)]
        public OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = _plc.Wait(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,bool,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,short,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,ushort,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,int,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,uint,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,long,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        [Obsolete("使用 WaitAsync(string,ulong,int,int,CancellationToken?)", true)]
        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1, CancellationToken? cancellation = null)
        {
            var result = await _plc.WaitAsync(address, waitValue, readInterval, waitTimeout, cancellation);
            //LogHelper.UiLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            LogHelper.PlcLog.Debug($"异步等待地址{address},类型{waitValue.GetType().Name},值{waitValue},等待结果{result.IsSuccess},等待时长{result.Content.TotalMinutes}ms");
            return result;
        }

        public OperateResult Write(string address, byte[] value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, bool[] value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, bool value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, short value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, short[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ushort value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ushort[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, int value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, int[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, uint value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, uint[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, long value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, long[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, ulong value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, ulong[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, float value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, float[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, double value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, double[] values)
        {
            var result = _plc.Write(address, values);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{values.GetType().Name},值{values.ToArrayString()}");
            return result;
        }

        public OperateResult Write(string address, string value)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, Encoding encoding)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, int length)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            return result;
        }

        public OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            var result = _plc.Write(address, value);
            //LogHelper.UiLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},编码格式{encoding},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"写入地址{address},类型{value.GetType().Name},值{value},长度{length},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, byte[] value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, bool[] value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, bool value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, short value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, short[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ushort value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ushort[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, int value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, int[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, uint value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, uint[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, long value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, long[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ulong value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, ulong[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, float value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, float[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, double value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, double[] values)
        {
            var result = await _plc.WriteAsync(address, values);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{values.GetType().Name},值{values},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, Encoding encoding)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, int length)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},长度{length},结果{result.IsSuccess}");
            return result;
        }

        public async Task<OperateResult> WriteAsync(string address, string value, int length, Encoding encoding)
        {
            var result = await _plc.WriteAsync(address, value);
            //LogHelper.UiLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},长度{length},结果{result.IsSuccess}");
            LogHelper.PlcLog.Debug($"异步写入地址{address},类型{value.GetType().Name},值{value},编码格式{encoding},长度{length},结果{result.IsSuccess}");
            return result;
        }
    }
}
