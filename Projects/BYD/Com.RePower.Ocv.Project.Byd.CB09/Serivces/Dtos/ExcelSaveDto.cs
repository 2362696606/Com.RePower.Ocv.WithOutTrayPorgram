using NPOI.Util.Collections;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos
{
    public class ExcelSaveDto
    {
        public int Position { get; set; }
        public string BarCode { get; set; } = string.Empty;
        public double Vol { get; set; }
        public double Res { get; set; }
        public double Temp { get; set; }
        public double NVolValue { get; set; }
        public double PVolValue { get; set; }
        public string NgResult { get; set; } = string.Empty;
        public string NgDescription { get; set; } = string.Empty;
    }
}
