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
        private OperateResult RequestAllLocateCellToWms()
        {
            if (SettingManager.CurrentOcvType == Model.Enums.OcvTypeEnum.Ocv0)
            {
                LogHelper.UiLog.Info("Ocv0调用Wms出库信号接口");
                var result = WmsService?.RequestAllLocateCellToWms() ?? OperateResult.CreateFailedResult<string>("调度服务实例为null");
                if (result.IsFailed)
                    return result;
                string str = result.Content ?? string.Empty;
                var obj = JsonConvert.DeserializeObject<WmsNormalReturnDto>(str) ?? new WmsNormalReturnDto();
                if (obj.Result != 1)
                    return OperateResult.CreateFailedResult($"OCV0出库接口调用失败:{obj.Message ?? "未知异常"}");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
