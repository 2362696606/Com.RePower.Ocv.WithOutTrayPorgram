using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv2Dto: MesBatteryResultForOcv1Dto
    {
        public string Ocv2Date { get; set; } = string.Empty;
        public string Ocv2 { get; set; } = string.Empty;
        public string Ocv2MinValue { get; set; } = string.Empty; 
        public string Ocv2MaxValue { get; set; } = string.Empty;
        public string Ocr2 { get; set; } = string.Empty;
        public string Ocr2MinValue { get; set; } = string.Empty;
        public string Ocr2MaxValue { get; set; } = string.Empty;
        public string K12 { get; set; } = string.Empty;
        public string K12MinValue { get; set; } = string.Empty;
        public string K12MaxValue { get; set; } = string.Empty;
        public string Ocv1Ocv2Internal { get; set; } = string.Empty;
        public string Ocv1Ocv2InternalMinValue { get; set; } = string.Empty;
        public string Ocv1Ocv2InternalMaxValue { get; set; } = string.Empty;
    }
}
