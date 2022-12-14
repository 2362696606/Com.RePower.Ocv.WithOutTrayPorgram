using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    public class OcvSettingItemDto
    {
        public int Id { get; set; }
        public string SettingName { get; set; } = string.Empty;
        public string JsonValue { get; set; } = string.Empty;
    }
}
