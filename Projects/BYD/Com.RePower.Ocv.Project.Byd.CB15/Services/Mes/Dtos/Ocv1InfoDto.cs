using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    [Table("OCV1BatData")]
    public class Ocv1InfoDto:OcvInfoDtoBase
    {
        /// <summary>
        /// OCV1值mV
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? Ocv1 { get; set; }
    }
}
