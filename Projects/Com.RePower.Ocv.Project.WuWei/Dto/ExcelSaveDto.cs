using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Dto
{
    public class ExcelSaveDto
    {
        public int Position { get; set; }
        public string BarCode { get; set; } = string.Empty;
        public double NVolValue { get; set; }
        public string NgResult { get; set; } = string.Empty;
        public string NgDescription { get; set; } = string.Empty;
    }
}
