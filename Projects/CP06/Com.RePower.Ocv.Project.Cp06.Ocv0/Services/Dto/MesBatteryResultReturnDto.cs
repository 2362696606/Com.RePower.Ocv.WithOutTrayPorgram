using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesBatteryResultReturnDto
    {
        public bool Status { get; set; }
        public object? Result { get; set; }
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
    }
}
