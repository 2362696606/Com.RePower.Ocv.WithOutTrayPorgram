using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos
{
    public class BatteryStandardSettingDto : Model.Settings.Dtos.BatteryStandardSettingDto
    {
        /// <summary>
        /// 压差最大值
        /// </summary>
        public double MaxVolDifference { get; set; }
        /// <summary>
        /// 压差最小值
        /// </summary>
        public double MinVolDifference { get; set; }
        /// <summary>
        /// 单托盘NgX值
        /// </summary>
        public double XValue { get; set; }
    }
}
