using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class Ocv1InfoForCvsDto : OcvInfoForCvsDtoBase
    {
        [Name("OCV1(mV)")]
        [Index(7)]
        public double? Vol { get; set; }
    }
}
