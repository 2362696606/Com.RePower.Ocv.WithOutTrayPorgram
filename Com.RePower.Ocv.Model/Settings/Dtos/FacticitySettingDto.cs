using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Settings.Dtos
{
    public class FacticitySettingDto
    {
        /// <summary>
        /// 是否真实Plc
        /// </summary>
        public bool IsRealPlc { get; set; }
        /// <summary>
        /// 是否真实内阻仪
        /// </summary>
        public bool IsRealOhm { get; set; }
        /// <summary>
        /// 是否真实万用表
        /// </summary>
        public bool IsRealDmm { get; set; }
        /// <summary>
        /// 是否是真实Mtvs
        /// </summary>
        public bool IsRealMtvs { get; set; }
        /// <summary>
        /// 是否真实切换板
        /// </summary>
        public bool IsRealSwitchBoard { get; set; }
        /// <summary>
        /// 是否是真实Wms
        /// </summary>
        public bool IsRealWms { get; set; }
        /// <summary>
        /// 是否是真实Mes
        /// </summary>
        public bool IsRealMes { get; set; }
    }
}
