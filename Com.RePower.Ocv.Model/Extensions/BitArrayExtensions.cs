using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Extensions
{
    public static class BitArrayExtensions
    {
        public static int ToInt32(this BitArray bits)
        {
            int[] res = new int[1];

            for (int i = 0; i < bits.Count; i++)
            {
                bits.CopyTo(res, 0);
            }

            return res[0];
        }
    }
}
