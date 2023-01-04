using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Dto
{
    public class TrayDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string TrayCode { get; set; } = string.Empty;
        /// <summary>
        /// Ng结果
        /// </summary>
        public virtual List<NgInfoDto> NgInfos { get; set; } = new List<NgInfoDto>();
    }
}
