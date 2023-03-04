using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesGetShopOrderListResultDto
    {
        public bool status { get; set; }
        public List<MesGetShopOrderListItem> result { get; set; } = new List<MesGetShopOrderListItem>();
        public string? errorCode { get; set; }
        public string? message { get; set; }
    }
    public class MesGetShopOrderListItem
    {
        public string value { get; set; } = string.Empty;
        public string text { get; set; } = string.Empty;
    }
}
