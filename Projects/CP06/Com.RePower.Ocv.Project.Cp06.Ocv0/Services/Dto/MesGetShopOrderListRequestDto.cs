using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto
{
    public class MesGetShopOrderListRequestDto
    {
        /// <summary>
        /// 站点
        /// </summary>
        public string? site { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public string? resourceNo { get; set; }
    }
}
