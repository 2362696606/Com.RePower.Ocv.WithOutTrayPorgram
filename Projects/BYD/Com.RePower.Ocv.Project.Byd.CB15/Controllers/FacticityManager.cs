using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers
{
    public class FacticityManager
    {
        /// <summary>
        /// 真实plc
        /// </summary>
        public bool IsRealPlc { get; set; }
        /// <summary>
        /// 真实内阻仪
        /// </summary>
        public bool IsRealOhm { get; set; }
        /// <summary>
        /// 真实万用表
        /// </summary>
        public bool IsRealDmm { get; set; }
        /// <summary>
        /// 真实切换板
        /// </summary>
        public bool IsRealSwitchBoard { get; set; }
        /// <summary>
        /// 真实wms
        /// </summary>
        public bool IsRealWms { get; set; }
        /// <summary>
        /// 真实mes
        /// </summary>
        public bool IsRealMes { get; set; }
        /// <summary>
        /// 真实正极温度传感器
        /// </summary>
        public bool IsRealPTemp { get; set; }
        /// <summary>
        /// 真实负极温度传感器
        /// </summary>
        public bool IsRealNTemp { get; set; }
    }
}
