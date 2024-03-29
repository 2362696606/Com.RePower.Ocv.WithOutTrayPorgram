﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms.Dto
{
    public class StationInfoDto
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipNum { get; set; } = string.Empty;
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string BarCode { get; set; } = string.Empty;
    }
}
