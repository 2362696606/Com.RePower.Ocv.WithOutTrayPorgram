using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Extensions
{
    public static class OperateResultConvert
    {
        public static WpfBase.OperateResult DoConvert(this HslCommunication.OperateResult source)
        {
            var result = new WpfBase.OperateResult();
            result.IsSuccess = source.IsSuccess;
            result.ErrorCode= source.ErrorCode;
            result.Message= source.Message;
            return result;
        }
        public static WpfBase.OperateResult<T> DoConvert<T>(this HslCommunication.OperateResult<T> source)
        {
            var result = new WpfBase.OperateResult<T>();
            result.IsSuccess = source.IsSuccess;
            result.ErrorCode = source.ErrorCode;
            result.Message = source.Message;
            result.Content = source.Content;
            return result;
        }
    }
}
