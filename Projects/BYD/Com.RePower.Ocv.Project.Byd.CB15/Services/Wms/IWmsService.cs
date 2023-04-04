using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsWebService;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms
{
    public interface IWmsService
    {
        /// <summary>
        /// 上传设备状态(可选)
        /// </summary>
        /// <returns></returns>
        public OperateResult<Status> UpdateOcvStatus();
        /// <summary>
        /// 获取电芯条码(必选)
        /// </summary>
        /// <returns></returns>
        public OperateResult<TrayInfoOCV> GetTechnologyInfoByBarCode();
        /// <summary>
        /// 上传测试结果(可选)
        /// </summary>
        /// <returns></returns>
        public OperateResult<Result> GetTestResultByTrayCode();
        /// <summary>
        /// 获取托盘类型(可选)
        /// </summary>
        /// <returns></returns>
        public OperateResult<TrayTypeOCV> GetTrayType();
    }
}
