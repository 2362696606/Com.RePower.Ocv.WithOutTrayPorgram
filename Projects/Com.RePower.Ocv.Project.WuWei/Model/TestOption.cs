using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Model
{
    public class TestOption
    {
        /// <summary>
        /// 是否复测
        /// </summary>
        public bool IsDoRetest { get; set; }
        /// <summary>
        /// 复测次数
        /// </summary>
        public int RetestTimes { get; set; }
        /// <summary>
        /// 是否上传结果到mes
        /// </summary>
        public bool IsDoUploadToMes { get; set; }
    }
}
