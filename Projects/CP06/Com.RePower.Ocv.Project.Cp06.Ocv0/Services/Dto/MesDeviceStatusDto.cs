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
        public string Site { get; set; } = string.Empty;
        public string MachineNo { get; set; } = string.Empty;
        public string Status { get; set; } = "01";
        public string Message { get; set; } = string.Empty;
        public string IsShutdown { get; set; } = "Y";
        [JsonProperty(PropertyName = "operator")]
        public string Operator { get; set; } = "未定义";
    }
}
