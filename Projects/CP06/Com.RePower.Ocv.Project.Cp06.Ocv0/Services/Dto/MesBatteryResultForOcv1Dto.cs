using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv1Dto: MesBatteryResultDto
    {
        public string Ocv1Date { get; set; } = string.Empty;
        public string Ocv1 { get; set; } = string.Empty;
        public string Ocv1MinValue { get; set; } = string.Empty;
        public string Ocv1MaxValue { get; set; } = string.Empty;
        public string Ocr1 { get; set; } = string.Empty;
        public string Ocr1MinValue { get; set; } = string.Empty; 
        public string Ocr1MaxValue { get; set; } = string.Empty;
    }
}
