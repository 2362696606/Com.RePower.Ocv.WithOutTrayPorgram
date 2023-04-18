using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultReturnDto
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("result")]
        public object? Result { get; set; }
        [JsonProperty("errorCode")]
        public string? ErrorCode { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
