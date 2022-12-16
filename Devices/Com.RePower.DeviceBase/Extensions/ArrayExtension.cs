using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.Extensions
{
    public static class ArrayExtension
    {
        public static List<T[]> SplitAry<T>(this T[] ary,int subSize)
        {
            int count = ary.Length % subSize == 0 ? ary.Length / subSize : ary.Length / subSize + 1;
            List<T[]> subAryList = new List<T[]>();
            for(int i = 0;i<count;i++)
            {
                int index = i* subSize;
                T[] subary = ary.Skip(index).Take(subSize).ToArray();
                subAryList.Add(subary);
            }
            return subAryList;
        }
    }
}
