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
        /// 最大正极侧边电压
        /// </summary>
        public double MaxPVol { get; set; }
        /// <summary>
        /// 最小正极侧边电压
        /// </summary>
        public double MinPVol { get; set; }
    }
}
