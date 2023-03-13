using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class Ocv4InfoForCvsDto : OcvInfoForCvsDtoBase
    {
        [Name("OC4(mV)")]
        [Index(7)]
        public double? Vol { get; set; }
        [Name("ACIR测试频率值")]
        public double? AcirTestFrequency { get; set; }
        [Name("ACIR拟合值")]
        public double? AcirFit { get; set; }
        [Name("ACIR原始值")]
        public double? AcirOriginal { get; set; }
        [Name("极差管控值")]
        public double? RangeControl { get; set; }
    }
}
