using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.Extensions
{
    public static class PlcExtensions
    {
        public static void PlcValueMonitor(this IPlc plc ,IEnumerable<PlcCacheValue> plcCaches,CancellationToken? token = null)
        {
            foreach(var item in plcCaches)
            {
                if (token?.IsCancellationRequested??false)
                    return;

            }
        }
    }
}
