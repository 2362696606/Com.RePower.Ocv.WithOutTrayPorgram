using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes
{
    public interface IMesService
    {
        /// <summary>
        /// 上传测试结果到mes
        /// </summary>
        /// <returns></returns>
        public OperateResult UploadResultToMes();
    }
}
