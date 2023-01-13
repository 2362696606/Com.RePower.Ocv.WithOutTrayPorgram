﻿namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class WmsBatteryInfo
    {
        public string BarCode { get; set; } = string.Empty;
        public int Index { get; set; }
        /// <summary>
        /// 电芯是否存在true为存在电芯，false不存在电芯
        /// </summary>
        public bool IsExist { get; set; }
    }
}