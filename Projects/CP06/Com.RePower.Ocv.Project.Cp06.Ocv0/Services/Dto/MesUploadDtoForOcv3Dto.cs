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
        public string LocationNo { get; set; } = string.Empty;
        /// <summary>
        /// 电芯条码    
        /// </summary>
        public string SfcNo { get; set; } = string.Empty;
        /// <summary>
        /// NG结果    
        /// </summary>
        public string DcResult { get; set; } = string.Empty; 
        /// <summary>
        /// NG原因    
        /// </summary>
        public string NgReason { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Ocv3Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// OCV3    
        /// </summary>
        public string Ocv3 { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最小值 
        /// </summary>
        public string Ocv3MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCV3最大值 
        /// </summary>
        public string Ocv3MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR3    
        /// </summary>
        public string Ocr3 { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最小值 
        /// </summary>
        public string Ocr3MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR3最大值 
        /// </summary>
        public string Ocr3MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// K23 
        /// </summary>
        public string K23 { get; set; } = string.Empty;
        /// <summary>
        /// K23最小值  
        /// </summary>
        public string K23MinValue { get; set; } = string.Empty;
        /// <summary>
        /// K23最大值 
        /// </summary>
        public string K23MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最小值    
        /// </summary>
        public string CccrMinValue { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比最大值   
        /// </summary>
        public string CccrMaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压值  
        /// </summary>
        public string SideVoltage { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最小值
        /// </summary>
        public string SideVoltageMinValue { get; set; } = string.Empty;
        /// <summary>
        /// 侧边电压最大值 
        /// </summary>
        public string SideVoltageMaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 档位（设备设定的档位）	
        /// </summary>
        public string LevelName { get; set; } = string.Empty;
    }
}
