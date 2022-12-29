using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.WpfBase.Extensions
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
        public static string ToHexString(this byte[] InBytes)
        {
            return InBytes.ToHexString('\0');
        }
        public static string ToHexString(this byte[] InBytes, char segment)
        {
            return InBytes.ToHexString(segment, 0);
        }
        public static string ToHexString(this byte[] InBytes, char segment, int newLineCount)
        {
            if (InBytes == null)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            foreach (byte b in InBytes)
            {
                if (segment == '\0')
                {
                    stringBuilder.Append($"{b:X2}");
                }
                else
                {
                    stringBuilder.Append($"{b:X2}{segment}");
                }

                num++;
                if (newLineCount > 0 && num >= newLineCount)
                {
                    stringBuilder.Append(Environment.NewLine);
                    num = 0L;
                }
            }

            if (segment != 0 && stringBuilder.Length > 1 && stringBuilder[stringBuilder.Length - 1] == segment)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }

            return stringBuilder.ToString();
        }
    }
}
