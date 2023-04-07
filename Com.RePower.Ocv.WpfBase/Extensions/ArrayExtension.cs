using System.Text;

namespace Com.RePower.Ocv.WpfBase.Extensions
{
    public static class ArrayExtension
    {
        public static List<T[]> SplitAry<T>(this T[] ary, int subSize)
        {
            int count = ary.Length % subSize == 0 ? ary.Length / subSize : ary.Length / subSize + 1;
            List<T[]> subAryList = new List<T[]>();
            for (int i = 0; i < count; i++)
            {
                int index = i * subSize;
                T[] subary = ary.Skip(index).Take(subSize).ToArray();
                subAryList.Add(subary);
            }
            return subAryList;
        }

        public static string ArrayFormat<T>(T[] array, string format)
        {
            if (array == null)
            {
                return "NULL";
            }

            StringBuilder stringBuilder = new StringBuilder("[");
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(string.IsNullOrEmpty(format) ? array[i]!.ToString() : string.Format(format, array[i]));
                if (i != array.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public static string ArrayFormat<T>(T[] array)
        {
            return ArrayFormat(array, string.Empty);
        }

        public static string ToArrayString<T>(this T[] value)
        {
            return ArrayFormat(value);
        }
    }
}