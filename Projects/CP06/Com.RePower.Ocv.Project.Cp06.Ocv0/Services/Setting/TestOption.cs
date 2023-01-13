using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting
{
    public class TestOption
    {
        /// <summary>
        /// 是否测试电压
        /// </summary>
        public bool IsTestVol { get; set; }
        /// <summary>
        /// 是否测试内阻
        /// </summary>
        public bool IsTestRes { get; set; }
        /// <summary>
        /// 是否测试负极壳电压
        /// </summary>
        public bool IsTestNVol { get; set; }
        /// <summary>
        /// 验证k值
        /// </summary>
        public bool VerifyKValue { get; set; }
        /// <summary>
        /// 是否复测
        /// </summary>
        public bool IsDoRetest { get; set; }
        /// <summary>
        /// 复测次数
        /// </summary>
        public int RetestTimes { get; set; }
        /// <summary>
        /// 负极壳体电压起始通道
        /// </summary>
        public int NVolStartChannel { get; set; }
        /// <summary>
        /// OCV3电压通道
        /// </summary>
        public int? VolChannelForOcv3 { get; set; }
        /// <summary>
        /// OCV3负极壳体电压通道
        /// </summary>
        public int? NVolChannelForOcv3 { get; set; }
    }
}
