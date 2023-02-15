using Com.RePower.Ocv.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos
{
    public class WmsResultDetails
    {
        /// <summary>
        /// 电池范围
        /// </summary>
        public Settings.Dtos.BatteryStandardSettingDto? BatteryStandard { get; set; }
        public List<NgInfoDto>? NgInfos { get; set; }
    }
}
