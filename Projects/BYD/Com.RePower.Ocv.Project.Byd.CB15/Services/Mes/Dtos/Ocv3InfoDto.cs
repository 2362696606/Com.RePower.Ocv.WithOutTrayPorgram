using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class Ocv3InfoDto:OcvInfoDtoBase
    {
        /// <summary>
        /// OCV2值mV
        /// </summary>
        [Column(TypeName = "DECIMAL(10,4)")]
        public decimal? OCV3 { get; set; }
    }
}
