using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class BatteryInfoDto
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BatteryCode { get; set; } = string.Empty;
        /// <summary>
        /// 位置
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// 0=OK  1=NG
        /// </summary>
        public int Result { get; set; }
    }
}
