﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    public class NgInfoDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 对应电池
        /// </summary>
        public virtual BatteryDto Battery { get; set; } = new BatteryDto();
        /// <summary>
        /// mg描述
        /// </summary>
        public string? NgDescription { get; set; }
        /// <summary>
        /// 是否ng
        /// </summary>
        public bool IsNg { get; set; } = false;
        /// <summary>
        /// ng类型
        /// </summary>
        public int? NgType { get; set; }
    }
}
