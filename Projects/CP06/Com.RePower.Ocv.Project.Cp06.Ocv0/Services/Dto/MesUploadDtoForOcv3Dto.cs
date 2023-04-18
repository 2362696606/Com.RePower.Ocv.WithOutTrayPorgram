using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesUploadDtoForOcv3Dto : MesUploadDto
    {
        /// <summary>
        /// 卡位号 
        /// </summary>
        [JsonProperty("LOCATION_NO")]
        public string LocationNo { get; set; } = string.Empty;
        /// <summary>
        /// 电芯条码    
        /// </summary>
        [JsonProperty("SFC_NO")]
        public string SfcNo { get; set; } = string.Empty;
        /// <summary>
        /// NG结果    
        /// </summary>
        [JsonProperty("DC_RESULT")]
        public string DcResult { get; set; } = string.Empty;
        /// <summary>
        /// NG原因    
        /// </summary>
        [JsonProperty("NG_REASON")]
        public string NgReason { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("OCV3_DATE")]
        public string Ocv3Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// OCV3    
        /// </summary>
        [JsonProperty("OCV3")]
        public string Ocv3 { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最小值 
        /// </summary>
        [JsonProperty("OCV3_MIN_VALUE")]
        public string Ocv3MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最大值 
        /// </summary>
        [JsonProperty("OCV3_MAX_VALUE")]
        public string Ocv3MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR3    
        /// </summary>
        [JsonProperty("OCR3")]
        public string Ocr3 { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最小值 
        /// </summary>
        [JsonProperty("OCR3_MIN_VALUE")]
        public string Ocr3MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最大值 
        /// </summary>
        [JsonProperty("OCR3_MAX_VALUE")]
        public string Ocr3MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// K23 
        /// </summary>
        [JsonProperty("K23")]
        public string K23 { get; set; } = string.Empty;
        /// <summary>
        /// K23最小值  
        /// </summary>
        [JsonProperty("K23_MIN_VALUE")]
        public string K23MinValue { get; set; } = string.Empty;
        /// <summary>
        /// K23最大值 
        /// </summary>
        [JsonProperty("K23_MAX_VALUE")]
        public string K23MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最小值    
        /// </summary>
        [JsonProperty("CCCR_MIN_VALUE")]
        public string CccrMinValue { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最大值   
        /// </summary>
        [JsonProperty("CCCR_MAX_VALUE")]
        public string CccrMaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压值  
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE")]
        public string SideVoltage { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最小值
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE_MIN_VALUE")]
        public string SideVoltageMinValue { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最大值 
        /// </summary>
        [JsonProperty("SIDE_VOLTAGE_MAX_VALUE")]
        public string SideVoltageMaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 档位（设备设定的档位）	
        /// </summary>
        [JsonProperty("LEVEL_NAME")]
        public string LevelName { get; set; } = string.Empty;
    }
}
