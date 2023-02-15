using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult ValidateNgInfoWithLocal(NgInfo ngInfo)
        {
            ngInfo.NgType = 0;
            if(SettingManager.CurrentTestOption?.IsTestVol??false)
            {
                if(ngInfo.Battery.VolValue>SettingManager.CurrentBatteryStandard?.MaxVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.电压过高);
                if (ngInfo.Battery.VolValue < SettingManager.CurrentBatteryStandard?.MinVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.电压过低);
            }
            if(SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                if (ngInfo.Battery.NVolValue > SettingManager.CurrentBatteryStandard?.MaxNVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.负极壳体电压过高);
                if (ngInfo.Battery.NVolValue < SettingManager.CurrentBatteryStandard?.MinNVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.负极壳体电压过低);
            }
            if(SettingManager.CurrentTestOption?.IsTestPVol??false)
            {
                if (ngInfo.Battery.PVolValue > SettingManager.CurrentBatteryStandard?.MaxPVol)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.正极壳体电压过高);
                if (ngInfo.Battery.PVolValue < SettingManager.CurrentBatteryStandard?.MinKValue)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.正极壳体电压过低);
            }
            if(SettingManager.CurrentTestOption?.IsTestRes??false)
            {
                if (ngInfo.Battery.Res > SettingManager.CurrentBatteryStandard?.MaxRes)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.内阻过高);
                if (ngInfo.Battery.Res < SettingManager.CurrentBatteryStandard?.MinRes)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.内阻过低);
            }
            if(SettingManager.CurrentTestOption?.IsTestTemp??false)
            {
                if (ngInfo.Battery.Temp > SettingManager.CurrentBatteryStandard?.MaxTemp)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.温度过高);
                if (ngInfo.Battery.Temp < SettingManager.CurrentBatteryStandard?.MinTemp)
                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.温度过低);
            }
            ngInfo.SetIsNg();
            ngInfo.SetNgDescritpion();
            return OperateResult.CreateSuccessResult();
        }
    }
}
