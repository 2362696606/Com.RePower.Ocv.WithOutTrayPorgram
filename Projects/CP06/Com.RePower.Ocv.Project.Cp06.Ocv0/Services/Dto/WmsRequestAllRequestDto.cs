using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class WmsRequestAllRequestDto
    {
        public string WhCode { get; set; } = string.Empty;
        public string TrayBarcode { get; set; } = string.Empty;
        public string Location { get; set; } = "OCVStation:1_1_1_F01";
        public string ProjectCode { get; set; } = "CP06";
    }
}
