using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class Ocv4InfoDto:OcvInfoDtoBase
    {
        
        /// <summary>
        /// OCV4V1值mV
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? Ocv4V1 { get; set; }
        /// <summary>
        /// OCV4V2值mV 电压补偿后的值
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? Ocv4V2 { get; set; }
        /// <summary>
        /// ACIR修正值mΩ
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? AcirR { get; set; }
        /// <summary>
        /// ACIR原始值mΩ
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? AcirRo { get; set; }
        /// <summary>
        /// 极差mΩ
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? AcirD { get; set; }
        /// <summary>
        /// ACIR检测结果
        /// </summary>
        [Column(TypeName = "varchar(2)")]
        public string? AcirCode { get; set; }
        
    }
}
