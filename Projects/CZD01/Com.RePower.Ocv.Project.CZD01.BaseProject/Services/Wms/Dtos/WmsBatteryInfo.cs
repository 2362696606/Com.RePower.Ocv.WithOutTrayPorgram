﻿namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsBatteryInfo
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
        /// <summary>
        /// 电芯位置
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 电芯是否存在true为存在电芯，false不存在电芯
        /// </summary>
        public bool IsExist { get; set; }
        /// <summary>
        /// 前一工站Ng信息
        /// </summary>
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