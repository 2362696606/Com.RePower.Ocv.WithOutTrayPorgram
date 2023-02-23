namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsBatteryInfo
    {
        public string BarCode { get; set; } = string.Empty;
        public int Index { get; set; }
        /// <summary>
        /// 电芯是否存在true为存在电芯，false不存在电芯
        /// </summary>
        public bool IsExist { get; set; }
        public WmsAttachedNgInfo? AttachedNgInfo { get; set; }
    }
    public class WmsAttachedNgInfo
    {
        /// <summary>
        /// 是否ng,true为正常，false为ng，null未无前一工站
        /// </summary>
        public bool? IsOk { get; set; }
        /// <summary>
        /// 前一工站
        /// </summary>
        public string? PreviousStation { get; set; }
        /// <summary>
        /// ng描述
        /// </summary>
        public string? Message { get; set; }
    }
}