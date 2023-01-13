using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv1Dto: MesBatteryResultDto
    {
        public string OCV1_DATE { get; set; } = string.Empty;
        public string OCV1 { get; set; } = string.Empty;
        public string OCV1_MIN_VALUE { get; set; } = string.Empty;
        public string OCV1_MAX_VALUE { get; set; } = string.Empty;
        public string OCR1 { get; set; } = string.Empty;
        public string OCR1_MIN_VALUE { get; set; } = string.Empty; 
        public string OCR1_MAX_VALUE { get; set; } = string.Empty;
    }
}
