using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings.Dtos
{
    public class TestOptionDto:Model.Settings.Dtos.TestOptionDto
    {
        /// <summary>
        /// 负极壳体电压起始通道
        /// </summary>
        public int? NVolStartChannel { get; set; }
        /// <summary>
        /// OCV3电压通道
        /// </summary>
        public int? VolChannelForOcv3 { get; set; }
        /// <summary>
        /// OCV3负极壳体电压通道
        /// </summary>
        public int? NVolChannelForOcv3 { get; set; }
        /// <summary>
        /// 电压ng通道
        /// </summary>
        public int? VolNgChannel { get; set; }
        /// <summary>
        /// 内阻ng通道
        /// </summary>
        public int? ResNgChannel { get; set; }
        /// <summary>
        /// K值ng通道
        /// </summary>
        public int? KValueNgChannel { get; set; }
        /// <summary>
        /// 负极壳体电压ng通道
        /// </summary>
        public int? NVolNgChannel { get; set; }
    }
}
