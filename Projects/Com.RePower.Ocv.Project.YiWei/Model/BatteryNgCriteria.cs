using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Model
{
    public class BatteryNgCriteria
    {
        /// <summary>
        /// 最大电压
        /// </summary>
        public double MaxVol { get; set; }
        /// <summary>
        /// 最小电压
        /// </summary>
        public double MinVol { get; set; }
        /// <summary>
        /// 最大内阻
        /// </summary>
        public double MaxRes { get; set; }
        /// <summary>
        /// 最小内阻
        /// </summary>
        public double MinRes { get; set; }
    }
}
