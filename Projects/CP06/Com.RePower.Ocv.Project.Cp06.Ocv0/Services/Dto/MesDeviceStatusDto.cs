using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesDeviceStatusDto
    {
        public string site { get; set; } = string.Empty;
        public string machineNo { get; set; } = string.Empty;
        public string status { get; set; } = "01";
        public string message { get; set; } = string.Empty;
        public string isShutdown { get; set; } = "Y";
        [JsonProperty(PropertyName = "operator")]
        public string Operator { get; set; } = "未定义";
    }
}
