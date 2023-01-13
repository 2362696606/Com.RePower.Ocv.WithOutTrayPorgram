using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv2Dto: MesBatteryResultForOcv1Dto
    {
        public string OCV2_DATE { get; set; } = string.Empty;
        public string OCV2 { get; set; } = string.Empty;
        public string OCV2_MIN_VALUE { get; set; } = string.Empty; 
        public string OCV2_MAX_VALUE { get; set; } = string.Empty;
        public string OCR2 { get; set; } = string.Empty;
        public string OCR2_MIN_VALUE { get; set; } = string.Empty;
        public string OCR2_MAX_VALUE { get; set; } = string.Empty;
        public string K12 { get; set; } = string.Empty;
        public string K12_MIN_VALUE { get; set; } = string.Empty;
        public string K12_MAX_VALUE { get; set; } = string.Empty;
        public string OCV1_OCV2_INTERNAL { get; set; } = string.Empty;
        public string OCV1_OCV2_INTERNAL_MIN_VALUE { get; set; } = string.Empty;
        public string OCV1_OCV2_INTERNAL_MAX_VALUE { get; set; } = string.Empty;
    }
}
