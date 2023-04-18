using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultForOcv0Dto:MesBatteryResultDto
    {

        /// <summary>
        /// ocv0时间
        /// </summary>
        [JsonProperty("OCV0_DATE")]
        public string Ocv0Date { get; set; } = string.Empty;
    }
}
