using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Serivces.Module
{
    public class WmsSetting
    {
        /// <summary>
        /// 获取托盘条码接口
        /// </summary>
        public string GetBatteryInfoUrl { get; set; } = "RequetEquipmentStationBingAsset";
        /// <summary>
        /// 上传测试结果接口
        /// </summary>
        public string UploadTestResultUrl { get; set; } = "UploadOCVTestResult";
        /// <summary>
        /// 基础地址
        /// </summary>
        public string BaseAddress { get; set; } = "http://172.17.20.6:8082/swagger/index.html/api/MERequest"; //"https://da49d2a1-05ff-4fcd-b537-2f74d2290138.mock.pstmn.io";
        /// <summary>
        /// 工艺号
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 工位号
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;
    }
}
