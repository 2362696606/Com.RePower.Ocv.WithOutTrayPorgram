using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting
{
    public class BatteryStandard
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
        /// <summary>
        /// 最大负极壳体电压
        /// </summary>
        public double MaxNVol { get; set; }
        /// <summary>
        /// 最小负极壳体电压
        /// </summary>
        public double MinNVol { get; set; }
        /// <summary>
        /// 最大K值
        /// </summary>
        public double MaxKValue { get; set; }
        /// <summary>
        /// 最小K值
        /// </summary>
        public double MinKValue { get; set; }
    }
}
