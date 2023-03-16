using Com.RePower.Ocv.Model.Extensions;
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
        private OperateResult GetCurrentTrayRange()
        {
            if (SettingManager.CurrentOcvType == Enums.OcvTypeEnmu.OCV4 && SettingManager.AcirOption is { } acirOption && (SettingManager.AcirOption?.IsAcirEnable ?? false))
            {
                var okNgInfos = this.Tray.NgInfos.Where(x => x.IsNg == false).ToList();
                var minRes = okNgInfos.Select(x => x.Battery.Res).Min();
                var standard = (decimal?)minRes + acirOption.CurrentTrayRequirements;
                if(standard > acirOption.DcirMax)
                    standard = acirOption.DcirMax;
                foreach(var item in okNgInfos)
                {
                    if(item.Battery.Res>(double?)standard || item.Battery.Res< (double?)acirOption.DcirMin)
                    {
                        item.AttachedNgDescription += "同盘管控Ng";
                        item.AttachedIsNg = true;
                    }
                }
            }

            return OperateResult.CreateSuccessResult();
        }
    }
}
