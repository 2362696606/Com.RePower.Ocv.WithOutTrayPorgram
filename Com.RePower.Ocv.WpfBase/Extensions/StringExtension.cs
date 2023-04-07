namespace Com.RePower.Ocv.WpfBase.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 将16进制字符串转为byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string value)
        {
            MemoryStream memoryStream = new MemoryStream();
            for (int i = 0; i < value.Length; i++)
            {
                if (i + 1 < value.Length && GetHexCharIndex(value[i]) >= 0 && GetHexCharIndex(value[i + 1]) >= 0)
                {
                    memoryStream.WriteByte((byte)(GetHexCharIndex(value[i]) * 16 + GetHexCharIndex(value[i + 1])));
                    i++;
                }
            }

            byte[] result = memoryStream.ToArray();
            memoryStream.Dispose();
            return result;
        }

        private static int GetHexCharIndex(char ch)
        {
            switch (ch)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;

                case 'A':
                case 'a':
                    return 10;

                case 'B':
                case 'b':
                    return 11;

                case 'C':
                case 'c':
                    return 12;

                case 'D':
                case 'd':
                    return 13;

                case 'E':
                case 'e':
                    return 14;

                case 'F':
                case 'f':
                    return 15;

                default:
                    return -1;
            }
        }
    }
}