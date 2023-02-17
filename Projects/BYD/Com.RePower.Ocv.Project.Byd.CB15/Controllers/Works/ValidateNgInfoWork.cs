using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Enums;
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
        /// <summary>
        /// 验证Ng
        /// </summary>
        /// <returns></returns>
        private OperateResult ValidateNgInfo()
        {
            foreach(var item in Tray.NgInfos)
            {
                var result = ValidateOneNgInfo(item);
                if(result.IsFailed)
                    return result;
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult ValidateOneNgInfo(NgInfo ngInfo)
        {
            bool isTestVol = SettingManager.CurrentTestOption?.IsTestVol ?? false;
            bool isTestRes = SettingManager.CurrentTestOption?.IsTestRes ?? false;
            bool isTestNVol = SettingManager.CurrentTestOption?.IsTestNVol ?? false;
            ngInfo.NgType = 0;
            if(isTestVol)
            {
                double maxVol = SettingManager.CurrentBatteryStandard?.MaxVol ?? double.MaxValue;
                double minVol = SettingManager.CurrentBatteryStandard?.MinVol ?? double.MinValue;
                if (ngInfo.Battery.VolValue > maxVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.电压过高);
                else if (ngInfo.Battery.VolValue < minVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.电压过低);
            }
            if(isTestRes)
            {
                double maxRes = SettingManager.CurrentBatteryStandard?.MaxRes ?? double.MaxValue;
                double minRes = SettingManager.CurrentBatteryStandard?.MinRes ?? double.MinValue;
                if (ngInfo.Battery.Res > maxRes)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.内阻过高);
                else if (ngInfo.Battery.Res < minRes)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.内阻过低);
            }
            if(isTestNVol)
            {
                double maxNVol = SettingManager.CurrentBatteryStandard?.MaxNVol ?? double.MaxValue;
                double minNVol = SettingManager.CurrentBatteryStandard?.MinNVol ?? double.MinValue;
                if (ngInfo.Battery.NVolValue > maxNVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.负极壳体电压过高);
                else if (ngInfo.Battery.NVolValue < minNVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.负极壳体电压过低);
            }
            //ngInfo.SetNgDescritpion();
            //ngInfo.SetIsNg();
            return OperateResult.CreateSuccessResult();
        }
    }
}
