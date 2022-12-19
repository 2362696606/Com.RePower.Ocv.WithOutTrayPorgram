using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Serivces.Dto
{
    public class WmsBatteriesInfoDto
    {
        public int Result { get; set; }
        public string? Message { get; set; }
        public PileContent? PileContent { get; set; }
    }
    public class PileContent
    {
        public string PilletBarcode { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string BatteryType { get; set; } = string.Empty;
        public List<OneBattery> Batterys { get; set; } = new List<OneBattery>();
    }

    public class OneBattery
    {
        public string BatteryBarcode { get; set; } = string.Empty;
        public int PalletIndex { get; set; }
        public int IsRealBattery { get; set; }
        public int IsNg { get; set; }
    }
}
