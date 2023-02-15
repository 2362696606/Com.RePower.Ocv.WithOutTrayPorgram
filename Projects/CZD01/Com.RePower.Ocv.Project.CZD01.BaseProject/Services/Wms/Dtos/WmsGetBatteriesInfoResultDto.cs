using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsGetBatteriesInfoResultDto
    {
        public int Result { get; set; }
        public string Message { get; set; } = string.Empty;
        public WmsHandleResultDto HandleResult { get; set; } = new WmsHandleResultDto();
    }
}
