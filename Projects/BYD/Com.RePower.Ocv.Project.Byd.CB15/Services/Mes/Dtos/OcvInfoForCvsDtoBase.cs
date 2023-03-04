using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos
{
    public class OcvInfoForCvsDtoBase
    {
        [Name("序号")]
        [Index(0)]
        public int Index { get; set; }
        [Name("上传时间")]
        [Index(1)]
        public DateTime UploadTime { get; set; }
        [Name("电芯条码")]
        [Index(2)]
        public string BarCode { get; set; } = string.Empty;
        [Name("结束时间")]
        [Index(3)]
        public DateTime EndTime { get; set; } = DateTime.Now;
        [Name("托盘条码")]
        [Index(4)]
        public string TrayCode { get; set; } = string.Empty;
        [Name("托盘层")]
        [Index(5)]
        public int TrayStation { get; set; } = 1;
        [Name("电芯位置")]
        [Index(6)]
        public int Position { get; set; }
        [Name("负对壳电压(mV)")]
        [Index(8)]
        public double? NVol { get; set; }
        [Name("正对壳电压(mV)")]
        [Index(9)]
        public double? PVol { get; set; }
        [Name("电芯温度(℃)")]
        [Index(10)]
        public double? Temp { get; set; }
        [Name("结束代码")]
        [Index(11)]
        public string EndCode { get; set; } = string.Empty;
        [Name("电脑编号")]
        [Index(12)]
        public string PcCode { get; set; } = SettingManager.Instance.CurrentMesSetting?.PcId ?? string.Empty;
        [Name("PC位置")]
        [Index(13)]
        public string? PcLocation { get; set; } = string.Empty;
    }
}
