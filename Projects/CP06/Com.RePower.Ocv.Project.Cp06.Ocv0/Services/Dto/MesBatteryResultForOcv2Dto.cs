using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv2Dto: MesBatteryResultForOcv1Dto
    {
        [JsonProperty("OCV2_DATE")]
        public string Ocv2Date { get; set; } = string.Empty;
        [JsonProperty("OCV2")]
        public string Ocv2 { get; set; } = string.Empty;
        [JsonProperty("OCV2_MIN_VALUE")]
        public string Ocv2MinValue { get; set; } = string.Empty;
        [JsonProperty("OCV2_MAX_VALUE")]
        public string Ocv2MaxValue { get; set; } = string.Empty;
        [JsonProperty("OCR2")]
        public string Ocr2 { get; set; } = string.Empty;
        [JsonProperty("OCR2_MIN_VALUE")]
        public string Ocr2MinValue { get; set; } = string.Empty;
        [JsonProperty("OCR2_MAX_VALUE")]
        public string Ocr2MaxValue { get; set; } = string.Empty;
        [JsonProperty("K12")]
        public string K12 { get; set; } = string.Empty;
        [JsonProperty("K12_MIN_VALUE")]
        public string K12MinValue { get; set; } = string.Empty;
        [JsonProperty("K12_MAX_VALUE")]
        public string K12MaxValue { get; set; } = string.Empty;
        //[JsonProperty("OCV2_MIN_VALUE")]
        public string Ocv1Ocv2Internal { get; set; } = string.Empty;
        //[JsonProperty("OCV2_MIN_VALUE")]
        public string Ocv1Ocv2InternalMinValue { get; set; } = string.Empty;
        //[JsonProperty("OCV2_MIN_VALUE")]
        public string Ocv1Ocv2InternalMaxValue { get; set; } = string.Empty;
    }
}
