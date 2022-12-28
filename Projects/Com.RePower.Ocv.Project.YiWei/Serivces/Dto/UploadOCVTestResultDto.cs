using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Serivces.Dto
{
    public class UploadOCVTestResultDto
    {
        /// <summary>
        /// 工位号
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;
        /// <summary>
        /// 托盘条码
        /// </summary>
        public string PalletBarcode { get; set; } = string.Empty;
        /// <summary>
        /// 电池测试结果列表
        /// </summary>
        public List<OneBatteryTestResult> BatteryTestResults { get; set; } = new List<OneBatteryTestResult>();
        /// <summary>
        /// 工艺号
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 托盘OK、NG标识1=OK；0=NG
        /// </summary>
        public int BatteryTestFlag { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        public string BatteryType { get; set; } = string.Empty;
    }
    public class OneBatteryTestResult
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string BatteryBarcode { get; set; } = string.Empty;
        /// <summary>
        /// 0:Ng,1:Ok
        /// </summary>
        public int Rseult { get; set; }
        /// <summary>
        /// NG代码
        /// </summary>
        public string? BatteryNGCode { get; set; }
        /// <summary>
        /// 电池位置
        /// </summary>
        public int BatteryIndex { get; set; }
    }
}
