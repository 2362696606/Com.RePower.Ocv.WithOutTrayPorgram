using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult UploadTestResultToMes()
        {
            LogHelper.UiLog.Info("上传到Mes");
            if(MesService == null)
            {
                LogHelper.UiLog.Warn("Mes服务为null，未上传mes数据");
            }
            else
            {
                var uploadResult = this.MesService?.UploadResultToMes();
                if (uploadResult?.IsFailed ?? true)
                    return uploadResult ?? OperateResult.CreateFailedResult("Mes服务为Null");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
