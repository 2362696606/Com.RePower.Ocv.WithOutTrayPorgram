using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
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
        private OperateResult UnBindingTray()
        {
            foreach(var item in Tray.NgInfos)
            {
                int sendValue = 1;
                if (!item.Battery.IsExsit)
                {
                    sendValue = 1;
                }
                else if(item.AttachedIsNg??false)
                {
                    sendValue = 2;
                }
                else if (SettingManager.CurrentOcvType ==  Model.Enums.OcvTypeEnum.OCV3)
                {
                    if (item.IsNg)
                    {
                        if ((SettingManager.CurrentTestOption?.IsTestTemp ?? false) ||
                            (SettingManager.CurrentTestOption?.IsTestNTemp ?? false) ||
                            (SettingManager.CurrentTestOption?.IsTestPTemp ?? false))
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.温度过高 | Model.Enums.NgTypeEnum.温度过低 |
                                               Model.Enums.NgTypeEnum.正极温度过高 | Model.Enums.NgTypeEnum.正极温度过低 |
                                               Model.Enums.NgTypeEnum.负极温度过高 | Model.Enums.NgTypeEnum.负极温度过低))
                                sendValue = SettingManager.CurrentOtherSetting?.TempNgChannel ?? 2;
                        }
                        if (SettingManager.CurrentTestOption?.IsVerifyCurrentKValue ?? false)
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.单托盘k值过低 | Model.Enums.NgTypeEnum.单托盘k值过高 | Model.Enums.NgTypeEnum.K值计算失败))
                                sendValue = SettingManager.CurrentOtherSetting?.PalletKNgChannel ?? 5;
                        }
                        if (SettingManager.CurrentTestOption?.IsVerifyKValue ?? false)
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.整体k值过低 | Model.Enums.NgTypeEnum.整体k值过高 | Model.Enums.NgTypeEnum.K值计算失败))
                                sendValue = SettingManager.CurrentOtherSetting?.IntegralKNgChannel ?? 5;
                        }
                        if (SettingManager.CurrentTestOption?.IsVerifyVolDifference ?? false)
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.压差计算失败 | Model.Enums.NgTypeEnum.压差过低 | Model.Enums.NgTypeEnum.压差过高))
                                sendValue = SettingManager.CurrentOtherSetting?.VolDifferenceNgChannel ?? 4;
                        }
                        if ((SettingManager.CurrentTestOption?.IsTestVol ?? false)
                            || (SettingManager.CurrentTestOption?.IsTestNVol ?? false)
                            || (SettingManager.CurrentTestOption?.IsTestPVol ?? false))
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.电压过低 | Model.Enums.NgTypeEnum.电压过高
                                | Model.Enums.NgTypeEnum.负极壳体电压过低 | Model.Enums.NgTypeEnum.负极壳体电压过高
                                | Model.Enums.NgTypeEnum.正极壳体电压过低 | Model.Enums.NgTypeEnum.正极壳体电压过高))
                                sendValue = SettingManager.CurrentOtherSetting?.VolNgChannel ?? 3;
                        }
                        if (SettingManager.CurrentTestOption?.IsTestRes ?? false)
                        {
                            if (item.HasNgType(Model.Enums.NgTypeEnum.内阻过低 | Model.Enums.NgTypeEnum.内阻过高))
                                sendValue = SettingManager.CurrentOtherSetting?.ResNgChannel ?? 2;
                        }

                        if (sendValue == 1)
                            sendValue = 2;
                    }
                }
                else
                {
                    sendValue = item.IsNg ? 2 : 1;
                }
                int posion = item.Battery.Position;
                LogHelper.UiLog.Info($"下发电池{posion}结果");
                string address = SettingManager.PlcValueCacheSetting?[$"位置{posion}托盘电池状态"]?.Address ?? string.Empty;
                if (string.IsNullOrEmpty(address))
                    return OperateResult.CreateFailedResult($"无法获取\"位置{posion}托盘电池状态\"地址");
                var writeResult = DevicesController.Plc?.Write(address, (short)sendValue) ?? OperateResult.CreateFailedResult("Plc实例为null");
                if (writeResult.IsFailed)
                {
                    return writeResult;
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
