using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesUploadDtoForOcv3Dto : MesUploadDto
    {
        /// <summary>
        /// 卡位号 
        /// </summary>
        public string LOCATION_NO { get; set; } = string.Empty;
        /// <summary>
        /// 电芯条码    
        /// </summary>
        public string SFC_NO { get; set; } = string.Empty;
        /// <summary>
        /// NG结果    
        /// </summary>
        public string DC_RESULT { get; set; } = string.Empty; 
        /// <summary>
        /// NG原因    
        /// </summary>
        public string NG_REASON { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OCV3_DATE { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// OCV3    
        /// </summary>
        public string OCV3 { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最小值 
        /// </summary>
        public string OCV3_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最大值 
        /// </summary>
        public string OCV3_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCR3    
        /// </summary>
        public string OCR3 { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最小值 
        /// </summary>
        public string OCR3_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最大值 
        /// </summary>
        public string OCR3_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// K23 
        /// </summary>
        public string K23 { get; set; } = string.Empty;
        /// <summary>
        /// K23最小值  
        /// </summary>
        public string K23_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// K23最大值 
        /// </summary>
        public string K23_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最小值    
        /// </summary>
        public string CCCR_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最大值   
        /// </summary>
        public string CCCR_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压值  
        /// </summary>
        public string SIDE_VOLTAGE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最小值
        /// </summary>
        public string SIDE_VOLTAGE_MIN_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最大值 
        /// </summary>
        public string SIDE_VOLTAGE_MAX_VALUE { get; set; } = string.Empty;
        /// <summary>
        /// 档位（设备设定的档位）	
        /// </summary>
        public string LEVEL_NAME { get; set; } = string.Empty;
    }
}
