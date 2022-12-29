using Com.RePower.DeviceBase.Attribute;
using Com.RePower.DeviceBase.Models;
using Com.RePower.Ocv.WpfBase;
using Com.RePower.WpfBase;
using System.Text;

namespace Com.RePower.DeviceBase.Plc
{
    /// <summary>
    /// Plc
    /// </summary>
    [DeviceInfo(DeviceType.PLC)]
    public interface IPlc : IDevice
    {
        /// <summary>
        /// 读byte[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<byte[]> Read(string address, ushort length);
        /// <summary>
        /// 写byte[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, byte[] value);
        /// <summary>
        /// 读bool[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<bool[]> ReadBool(string address, ushort length);
        /// <summary>
        /// 读bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<bool> ReadBool(string address);
        /// <summary>
        /// 写bool[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>读取结果</returns>
        OperateResult Write(string address, bool[] value);
        /// <summary>
        /// 写bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>读取结果</returns>
        OperateResult Write(string address, bool value);
        /// <summary>
        /// 读Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<short> ReadInt16(string address);
        /// <summary>
        /// 读Int16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<short[]> ReadInt16(string address, ushort length);
        /// <summary>
        /// 读UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<ushort> ReadUInt16(string address);
        /// <summary>
        /// 读UInt16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<ushort[]> ReadUInt16(string address, ushort length);
        /// <summary>
        /// 读Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<int> ReadInt32(string address);
        /// <summary>
        /// 读Int32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<int[]> ReadInt32(string address, ushort length);
        /// <summary>
        /// 读UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<uint> ReadUInt32(string address);
        /// <summary>
        /// 读UInt32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<uint[]> ReadUInt32(string address, ushort length);
        /// <summary>
        /// 读Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<long> ReadInt64(string address);
        /// <summary>
        /// 读Int64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        OperateResult<long[]> ReadInt64(string address, ushort length);
        /// <summary>
        /// 读UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<ulong> ReadUInt64(string address);
        /// <summary>
        /// 读UInt64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<ulong[]> ReadUInt64(string address, ushort length);
        /// <summary>
        /// 读Float
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OperateResult<float> ReadFloat(string address);
        /// <summary>
        /// 读Float[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<float[]> ReadFloat(string address, ushort length);
        /// <summary>
        /// 读Double
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns></returns>
        OperateResult<double> ReadDouble(string address);
        /// <summary>
        /// 读Double[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<double[]> ReadDouble(string address, ushort length);
        /// <summary>
        /// 读String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        OperateResult<string> ReadString(string address, ushort length);
        /// <summary>
        /// 读String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>读取结果</returns>
        OperateResult<string> ReadString(string address, ushort length, Encoding encoding);
        /// <summary>
        /// 等待Bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, short waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, int waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, long waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 等待UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        OperateResult<TimeSpan> Wait(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待Bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 异步等待UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="waitValue">值</param>
        /// <param name="readInterval">读取间隔</param>
        /// <param name="waitTimeout">等待超时时间</param>
        /// <returns>等待结果带等待时长</returns>
        Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1);
        /// <summary>
        /// 写Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, short value);
        /// <summary>
        /// 写Int16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, short[] values);
        /// <summary>
        /// 写UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, ushort value);
        /// <summary>
        /// 写UInt16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, ushort[] values);
        /// <summary>
        /// 写Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, int value);
        /// <summary>
        /// 写Int32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, int[] values);
        /// <summary>
        /// 写UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, uint value);
        /// <summary>
        /// 写UInt32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, uint[] values);
        /// <summary>
        /// 写Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, long value);
        /// <summary>
        /// 写Int64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, long[] values);
        /// <summary>
        /// 写UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, ulong value);
        /// <summary>
        /// 写UInt64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, ulong[] values);
        /// <summary>
        /// 写Float
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, float value);
        /// <summary>
        /// 写Float[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, float[] values);
        /// <summary>
        /// 写Double
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, double value);
        /// <summary>
        /// 写Double[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, double[] values);
        /// <summary>
        /// 写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, string value);
        /// <summary>
        /// 写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, string value, Encoding encoding);
        /// <summary>
        /// 写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, string value, int length);
        /// <summary>
        /// 写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>写入结果</returns>
        OperateResult Write(string address, string value, int length, Encoding encoding);
        /// <summary>
        /// 异步读Byte[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<byte[]>> ReadAsync(string address, ushort length);
        /// <summary>
        /// 异步写Byte[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, byte[] value);
        /// <summary>
        /// 异步读Bool[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length);
        /// <summary>
        /// 异步读Bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<bool>> ReadBoolAsync(string address);
        /// <summary>
        /// 异步写Bool[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, bool[] value);
        /// <summary>
        /// 异步写Bool
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, bool value);
        /// <summary>
        /// 异步读Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<short>> ReadInt16Async(string address);
        /// <summary>
        /// 异步读Int16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<short[]>> ReadInt16Async(string address, ushort length);
        /// <summary>
        /// 异步读UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<ushort>> ReadUInt16Async(string address);
        /// <summary>
        /// 异步读UInt16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<ushort[]>> ReadUInt16Async(string address, ushort length);
        /// <summary>
        /// 异步读Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<int>> ReadInt32Async(string address);
        /// <summary>
        /// 异步读Int32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<int[]>> ReadInt32Async(string address, ushort length);
        /// <summary>
        /// 异步读UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<uint>> ReadUInt32Async(string address);
        /// <summary>
        /// 异步读UInt32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<uint[]>> ReadUInt32Async(string address, ushort length);
        /// <summary>
        /// 异步读Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<long>> ReadInt64Async(string address);
        /// <summary>
        /// 异步读Int64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<long[]>> ReadInt64Async(string address, ushort length);
        /// <summary>
        /// 异步读UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<ulong>> ReadUInt64Async(string address);
        /// <summary>
        /// 异步读UInt64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<ulong[]>> ReadUInt64Async(string address, ushort length);
        /// <summary>
        /// 异步读Float
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<float>> ReadFloatAsync(string address);
        /// <summary>
        /// 异步读Float[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<float[]>> ReadFloatAsync(string address, ushort length);
        /// <summary>
        /// 异步读Double
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<double>> ReadDoubleAsync(string address);
        /// <summary>
        /// 异步读Double[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<double[]>> ReadDoubleAsync(string address, ushort length);
        /// <summary>
        /// 异步读String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<string>> ReadStringAsync(string address, ushort length);
        /// <summary>
        /// 异步读String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>读取结果</returns>
        Task<OperateResult<string>> ReadStringAsync(string address, ushort length, Encoding encoding);
        /// <summary>
        /// 异步写Int16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, short value);
        /// <summary>
        /// 异步写Int16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, short[] values);
        /// <summary>
        /// 异步写UInt16
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, ushort value);
        /// <summary>
        /// 异步写UInt16[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, ushort[] values);
        /// <summary>
        /// 异步写Int32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, int value);
        /// <summary>
        /// 异步写Int32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, int[] values);
        /// <summary>
        /// 异步写UInt32
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, uint value);
        /// <summary>
        /// 异步写UInt32[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, uint[] values);
        /// <summary>
        /// 异步写Int64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, long value);
        /// <summary>
        /// 异步写Int64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, long[] values);
        /// <summary>
        /// 异步写UInt64
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, ulong value);
        /// <summary>
        /// 异步写UInt64[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, ulong[] values);
        /// <summary>
        /// 异步写Float
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, float value);
        /// <summary>
        /// 异步写Float[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, float[] values);
        /// <summary>
        /// 异步写Double
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, double value);
        /// <summary>
        /// 异步写Double[]
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, double[] values);
        /// <summary>
        /// 异步写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, string value);
        /// <summary>
        /// 异步写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, string value, Encoding encoding);
        /// <summary>
        /// 异步写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, string value, int length);
        /// <summary>
        /// 异步写String
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>写入结果</returns>
        Task<OperateResult> WriteAsync(string address, string value, int length, Encoding encoding);
        /// <summary>
        /// 异步读自定义类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>读取结果</returns>
        //Task<OperateResult<T>> ReadAsync<T>() where T : class, new();
        /// <summary>
        /// 异步写自定义类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>写入结果</returns>
        //Task<OperateResult> WriteAsync<T>(T data) where T : class, new();
    }
}
