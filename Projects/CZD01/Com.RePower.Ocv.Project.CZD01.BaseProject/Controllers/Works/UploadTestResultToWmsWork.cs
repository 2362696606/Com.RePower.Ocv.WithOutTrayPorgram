using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult UploadTestResultToWms()
        {
            LogHelper.UiLog.Info("上传OCV结果到WMS");
            var uploadResult = WmsService?.UploadTestResult() ?? OperateResult.CreateFailedResult<string>("Wms服务实例为null");
            if (uploadResult.IsFailed)
                return uploadResult;
            string recovertContentStr = uploadResult.Content ?? string.Empty;
            var obj = JsonConvert.DeserializeObject<WmsNormalReturnDto>(recovertContentStr) ?? new WmsNormalReturnDto();
            if (obj.Result != 1)
                return OperateResult.CreateFailedResult($"上传调度失败:{obj.Message ?? "未知异常"}");
            return OperateResult.CreateSuccessResult();
        }
    }
}
