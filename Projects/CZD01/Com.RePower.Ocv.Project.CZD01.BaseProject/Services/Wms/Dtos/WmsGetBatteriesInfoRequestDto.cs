using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsGetBatteriesInfoRequestDto
    {
        public string RequestLocation { get; set; } = string.Empty;
        public string TrayCode { get; set; } = string.Empty;
    }
}
