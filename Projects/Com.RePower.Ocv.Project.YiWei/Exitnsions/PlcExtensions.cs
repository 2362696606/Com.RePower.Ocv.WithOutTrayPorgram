using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Exitnsions
{
    public static class PlcExtensions
    {
        public static OperateResult<string> ReadValue(this IPlc plc, PlcCacheValue item)
        {
            if (!plc.IsConnected)
            {
                item.Value = "Plc尚未连接";
                return OperateResult.CreateFailedResult<string>("Plc尚未连接");
            }
            Type type = item.Type;
            OperateResult readResult = OperateResult.CreateFailedResult("尚未读取");
            string? readStr = null;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    {
                        readResult = plc.ReadBool(item.Address);
                        readStr = (readResult as OperateResult<bool>)?.Content.ToString();
                        break;
                    }
                case TypeCode.Int16:
                    {
                        readResult = plc.ReadInt16(item.Address);
                        readStr = (readResult as OperateResult<short>)?.Content.ToString();
                        break;
                    }
                case TypeCode.UInt16:
                    {
                        readResult = plc.ReadUInt16(item.Address);
                        readStr = (readResult as OperateResult<ushort>)?.Content.ToString();
                        break;
                    }
                case TypeCode.Int32:
                    {
                        readResult = plc.ReadInt32(item.Address);
                        readStr = (readResult as OperateResult<int>)?.Content.ToString();
                        break;
                    }
                case TypeCode.UInt32:
                    {
                        readResult = plc.ReadUInt32(item.Address);
                        readStr = (readResult as OperateResult<uint>)?.Content.ToString();
                        break;
                    }
                case TypeCode.Int64:
                    {
                        readResult = plc.ReadInt64(item.Address);
                        readStr = (readResult as OperateResult<long>)?.Content.ToString();
                        break;
                    }
                case TypeCode.UInt64:
                    {
                        readResult = plc.ReadUInt64(item.Address);
                        readStr = (readResult as OperateResult<ulong>)?.Content.ToString();
                        break;
                    }
                case TypeCode.Single:
                    {
                        readResult = plc.ReadFloat(item.Address);
                        readStr = (readResult as OperateResult<float>)?.Content.ToString();
                        break;
                    }
                case TypeCode.Double:
                    {
                        readResult = plc.ReadDouble(item.Address);
                        readStr = (readResult as OperateResult<double>)?.Content.ToString();
                        break;
                    }
                case TypeCode.String:
                    {
                        readResult = plc.ReadString(item.Address, (ushort)item.Length);
                        readStr = (readResult as OperateResult<string>)?.Content?.ToString();
                        break;
                    }
                default:
                    {
                        readResult = OperateResult.CreateFailedResult($"未实现类型{type.Name}的读取");
                        readStr = "未实现类型{type.Name}的读取";
                        break;
                    }
            }
            item.Value = readStr;
            if (readResult.IsFailed)
                return OperateResult.CreateFailedResult<string>(readResult.Message);
            else if (string.IsNullOrEmpty(readStr))
                return OperateResult.CreateFailedResult<string>("未能成功转换数值为string");
            else
                return OperateResult.CreateSuccessResult<string>(readStr);
        }
    }
}
