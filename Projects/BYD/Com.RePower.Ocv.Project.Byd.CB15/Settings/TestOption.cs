using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Setting
{
    public partial class TestOption:ObservableObject
    {
        [ObservableProperty]
        private string _a = "a";
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
        /// 托盘电池数量
        /// </summary>
        public int BatteryCount { get; set; }
    }
}
