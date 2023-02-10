using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Setting
{
    public partial class TestOption:Com.RePower.Ocv.Model.Settings.TestOption
    {
        /// <summary>
        /// 验证k值
        /// </summary>
        public bool VerifyKValue { get; set; }
        /// <summary>
        /// Msa测试次数
        /// </summary>
        public int MsaTimes { get; set; }
        /// <summary>
        /// 托盘电池数量
        /// </summary>
        public int BatteryCount { get; set; }
    }
}
