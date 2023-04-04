using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultDto
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
        /// ng结果
        /// </summary>
        public string DcResult { get; set; } = string.Empty; 
        /// <summary>
        /// ng原因
        /// </summary>
        public string NgReason { get; set; } = string.Empty;
        /// <summary>
        /// OCV0
        /// </summary>
        public string Ocv0 { get; set; } = string.Empty;
        /// <summary>
        /// OCV0最小值
        /// </summary>
        public string Ocv0MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCV0最大值
        /// </summary>
        public string Ocv0MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR0
        /// </summary>
        public string Ocr0 { get; set; } = string.Empty;
        /// <summary>
        /// OCR0最小值
        /// </summary>
        public string Ocr0MinValue { get; set; } = string.Empty;
        /// <summary>
        /// OCR0最大值
        /// </summary>
        public string Ocr0MaxValue { get; set; } = string.Empty;
        /// <summary>
        /// 恒流容量比
        /// </summary>
        public string Cccr { get; set; } = string.Empty;
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
        /// 档位
        /// </summary>
        public string LevelName { get; set; } = string.Empty;

    }
}
