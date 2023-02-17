namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class WmsBatteryInfo
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 位置
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 电芯是否存在true为存在电芯，false不存在电芯
        /// </summary>
        public bool IsExist { get; set; }
        /// <summary>
        /// 附加Ng信息
        /// </summary>
        public WmsAttachedNgInfo? AttachedNgInfo { get; set; }
    }
    public class WmsAttachedNgInfo
    {
        /// <summary>
        /// 是否ng,true为ng，false为正常
        /// </summary>
        public bool IsNg { get; set; }
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