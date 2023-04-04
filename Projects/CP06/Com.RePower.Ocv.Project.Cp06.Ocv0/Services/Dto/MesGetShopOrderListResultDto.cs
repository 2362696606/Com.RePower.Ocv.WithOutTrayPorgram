using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesGetShopOrderListResultDto
    {
        public bool Status { get; set; }
        public List<MesGetShopOrderListItem> Result { get; set; } = new List<MesGetShopOrderListItem>();
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
    }
    public class MesGetShopOrderListItem
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
