using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.WpfBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult VerifyKValue()
        {
            if (SettingManager.CurrentOcvType == Model.Enums.OcvTypeEnum.OCV3
                && ((SettingManager.CurrentTestOption?.IsVerifyKValue ?? false)
                || (SettingManager.CurrentTestOption?.IsVerifyVolDifference ?? false)
                || (SettingManager.CurrentTestOption?.IsVerifyCurrentKValue ?? false))) 
            {
                foreach (var ngInfo in Tray.NgInfos)
                {
                    if (SceneContext is { })
                    {
                        var ocv2InfoList = SceneContext.NgInfos.Where(x => x.Battery.BarCode == ngInfo.Battery.BarCode
                        && x.Battery.TaskCode == ngInfo.Battery.TaskCode
                        && x.Battery.OcvType == "OCV2")
                            .Include(x => x.Battery)?.ToList();
                        if (ocv2InfoList is { })
                        {
                            ocv2InfoList.Sort((x, y) => DateTime.Compare(x.Battery.TestTime, y.Battery.TestTime));
                            var ocv2Info = ocv2InfoList.Last();
                            if (SettingManager.CurrentTestOption?.IsVerifyKValue ?? false)
                            {
                                var ocv2Time = ocv2Info.Battery.TestTime;
                                var currentTestTime = ngInfo.Battery.TestTime;
                                var timeSpen = (currentTestTime - ocv2Time).TotalHours;
                                var kDifference = ocv2Info.Battery.VolValue - ngInfo.Battery.VolValue;
                                ngInfo.Battery.KValue1 = (kDifference / timeSpen);

                                if(ngInfo.Battery.KValue1>SettingManager.CurrentBatteryStandard?.MaxKValue)
                                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.整体k值过高);
                                if(ngInfo.Battery.KValue1<SettingManager.CurrentBatteryStandard?.MinKValue)
                                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.整体k值过低);
                            }

                            if (SettingManager.CurrentTestOption?.IsVerifyVolDifference ?? false) 
                            {
                                ngInfo.Battery.ReserveValue1 = (ocv2Info.Battery.VolValue - ngInfo.Battery.VolValue);
                                if (ngInfo.Battery.ReserveValue1 > SettingManager.CurrentBatteryStandard?.MaxVolDifference)
                                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.压差过高);
                                if (ngInfo.Battery.ReserveValue1 > SettingManager.CurrentBatteryStandard?.MinVolDifference)
                                    ngInfo.AddNgType(Model.Enums.NgTypeEnum.压差过低);
                            }
                        }
                        else
                        {
                            if (SettingManager.CurrentTestOption?.IsVerifyKValue ?? false)
                                ngInfo.AddNgType(Model.Enums.NgTypeEnum.K值计算失败);
                            if (SettingManager.CurrentTestOption?.IsVerifyVolDifference ?? false)
                                ngInfo.AddNgType(Model.Enums.NgTypeEnum.压差计算失败);
                        }
                    }
                    else
                    {
                        if(SettingManager.CurrentTestOption?.IsVerifyKValue??false)
                            ngInfo.AddNgType(Model.Enums.NgTypeEnum.K值计算失败);
                        if (SettingManager.CurrentTestOption?.IsVerifyVolDifference ?? false)
                            ngInfo.AddNgType(Model.Enums.NgTypeEnum.压差计算失败);
                    }
                    //ngInfo.SetIsNg();
                    //ngInfo.SetNgDescritpion();
                }
                if (SettingManager.CurrentTestOption?.IsVerifyCurrentKValue ?? false)
                {
                    var tempList1 = Tray.NgInfos.Select(x => x.Battery.KValue1);
                    List<double> sample = new List<double>();
                    foreach (var item in tempList1)
                    {
                        if (item != null)
                            sample.Add((double)item);
                    }
                    var standardDeviation = sample.StandardDeviation();
                    var avg = sample.Average();
                    var x = SettingManager.CurrentBatteryStandard?.XValue ?? 0.0;
                    var max = avg + (x * standardDeviation);
                    var min = avg - (x * standardDeviation);
                    foreach (var ngInfo in Tray.NgInfos)
                    {
                        if (ngInfo.Battery.KValue1 > max)
                            ngInfo.AddNgType(Model.Enums.NgTypeEnum.单托盘k值过高);
                        if (ngInfo.Battery.KValue1 < min)
                            ngInfo.AddNgType(Model.Enums.NgTypeEnum.单托盘k值过低);
                        //ngInfo.SetIsNg();
                        //ngInfo.SetNgDescritpion();
                    }
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
