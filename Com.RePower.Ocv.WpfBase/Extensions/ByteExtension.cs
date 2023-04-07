using System.Text;

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

        public static string ToHexString(this byte[] inBytes)
        {
            return inBytes.ToHexString('\0');
        }

        public static string ToHexString(this byte[] inBytes, char segment)
        {
            return inBytes.ToHexString(segment, 0);
        }

        public static string ToHexString(this byte[] inBytes, char segment, int newLineCount)
        {
            if (inBytes == null)
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            long num = 0L;
            foreach (byte b in inBytes)
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