using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class TrayTypeOcvDto
    {
        /// <summary>
        /// 1-空托盘  2-实托盘 0 未知(未知需要OCV界面报警)
        /// </summary>
        public int TrayType { get; set; }
        public WmsRecoveryStatusDto? Status { get; set; }
    }
}
