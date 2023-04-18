using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultDto
    {
        /// <summary>
        /// 卡位号
        /// </summary>
        [JsonProperty("LOCATION_NO")]
        public string LOCATION_NO { get; set; } = string.Empty;
        /// <summary>
        /// 电芯条码
        /// </summary>
        [JsonProperty("SFC_NO")]
        public string SFC_NO { get; set; } = string.Empty;
        /// <summary>
        /// ng结果
        /// </summary>
        [JsonProperty("DC_RESULT")]
        public string DC_RESULT { get; set; } = string.Empty;
        /// <summary>
        /// ng原因
        /// </summary>
        [JsonProperty("NG_REASON")]
        public string NG_REASON { get; set; } = string.Empty;
        /// <summary>
        /// OCV0
        /// </summary>
        [JsonProperty("OCV0")]
        public string OCV0 { get; set; } = string.Empty;
        /// <summary>
        /// OCV0最小值
        /// </summary>
        [JsonProperty("OCV0_MIN_VALUE")]
        public string OCV0_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCV0最大值
        /// </summary>
        [JsonProperty("OCV0_MAX_VALUE")]
        public string OCV0_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCR0
        /// </summary>
        [JsonProperty("OCR0")]
        public string OCR0 { get; set; } = string.Empty;
        /// <summary>
        /// OCR0最小值
        /// </summary>
        [JsonProperty("OCR0_MIN_VALUE")]
        public string OCR0_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCR0最大值
        /// </summary>
        [JsonProperty("OCR0_MAX_VALUE")]
        public string OCR0_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比
        /// </summary>
        [JsonProperty("CCCR")]
        public string CCCR { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最小值
        /// </summary>
        [JsonProperty("CCCR_MIN_VALUE")]
        public string CCCR_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最大值
        /// </summary>
        [JsonProperty("CCCR_MAX_VALUE")]
        public string CCCR_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压值
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE")]
        public string SIDE_VOLTAGE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最小值
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE_MIN_VALUE")]
        public string SIDE_VOLTAGE_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最大值
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE_MAX_VALUE")]
        public string SIDE_VOLTAGE_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 档位
        /// </summary>
        [JsonProperty("LEVEL_NAME")]
        public string LEVEL_NAME { get; set; } = string.Empty;

    }
}
