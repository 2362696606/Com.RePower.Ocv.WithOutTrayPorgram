using Com.RePower.Ocv.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos
{
    public class OtherSettingDto
    {
        /// <summary>
        /// 正极壳体电压开始通道
        /// 
        /// </summary>
        public int PVolStartChannel { get; set; }
        /// <summary>
        /// 负极壳体电压开始通道
        /// </summary>
        public int NVolStartChannel { get; set; }
        /// <summary>
        /// 电压Ng通道
        /// </summary>
        public int VolNgChannel { get; set; }
        /// <summary>
        /// 内阻Ng通道
        /// </summary>
        public int ResNgChannel { get; set; }
        /// <summary>
        /// 温度Ng通道
        /// </summary>
        public int TempNgChannel { get; set; }
        /// <summary>
        /// 整体K值Ng通道
        /// </summary>
        public int IntegralKNgChannel { get; set; }
        /// <summary>
        /// 单托盘K值Ng通道
        /// </summary>
        public int PalletKNgChannel { get; set; }
        /// <summary>
        /// 压差Ng通道
        /// </summary>
        public int VolDifferenceNgChannel { get; set; }

        /// <summary>
        /// 最大ng报警数
        /// </summary>
        public int MaxErrorNg { get; set; }
    }
}
