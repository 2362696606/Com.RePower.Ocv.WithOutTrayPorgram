using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.Extensions
{
    public static class ByteExtension
    {
        /// <summary>
        /// 获取异或校验值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte GetCheckXor(this byte[] data)
        {
            byte checkCode = 0;
            for (int i = 0; i < data.Count(); i++)
            {
                checkCode ^= data[i];
            }
            return checkCode;
        }
    }
}
