﻿namespace Com.RePower.Ocv.Model.Dto
{
    public class TrayDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 托盘条码
        /// </summary>
        public string TrayCode { get; set; } = string.Empty;

        /// <summary>
        /// 任务号
        /// </summary>
        public long? TaskCode { get; set; }

        /// <summary>
        /// Ng结果
        /// </summary>
        public virtual List<NgInfoDto> NgInfos { get; set; } = new List<NgInfoDto>();
    }
}