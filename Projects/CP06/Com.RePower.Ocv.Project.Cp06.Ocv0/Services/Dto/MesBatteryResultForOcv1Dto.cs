using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv1Dto: MesBatteryResultDto
    {
        [JsonProperty("OCV1_DATE")]
        public string Ocv1Date { get; set; } = string.Empty;
        [JsonProperty("OCV1")]
        public string Ocv1 { get; set; } = string.Empty;
        [JsonProperty("OCV1_MIN_VALUE")]
        public string Ocv1MinValue { get; set; } = string.Empty;
        [JsonProperty("OCV1_MAX_VALUE")]
        public string Ocv1MaxValue { get; set; } = string.Empty;
        [JsonProperty("OCR1")]
        public string Ocr1 { get; set; } = string.Empty;
        [JsonProperty("OCR1_MIN_VALUE")]
        public string Ocr1MinValue { get; set; } = string.Empty;
        [JsonProperty("OCR1_MAX_VALUE")]
        public string Ocr1MaxValue { get; set; } = string.Empty;
    }
}
