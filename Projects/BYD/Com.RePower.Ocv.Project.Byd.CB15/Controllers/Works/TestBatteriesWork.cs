using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Modules;
using Com.RePower.WpfBase;
using ICSharpCode.SharpZipLib.Zip;
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
        /// 测试所有电池
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult TestBatteries()
        {
            LogHelper.UiLog.Info("关闭切换板所有通道");
            var closeResult1 = DevicesController.SwitchBoard?.CloseAllChannels(1);
            if(closeResult1?.IsFailed??true)
                return closeResult1 ?? OperateResult.CreateFailedResult("关闭箱号1的切换板所有通道失败，因为切换板实例为null");
            var closeResult2 = DevicesController.SwitchBoard?.CloseAllChannels(2);
            if(closeResult2?.IsFailed??true)
                return closeResult2 ?? OperateResult.CreateFailedResult("关闭箱号2的切换板所有通道失败，因为切换板实例为null");
            foreach (var item in Tray.NgInfos)
            {
                DoPauseOrStop();
                var testResult = TestOneBattery(item);
                if(testResult.IsFailed)
                    return testResult;
                DoPauseOrStop();
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult TestOneBattery(NgInfo ngInfo)
        {
            if (ngInfo.Battery.IsTested && !ngInfo.IsNg && !_isMsaTest && !_lastTimesIsMsa) 
            {
                return OperateResult.CreateSuccessResult();
            }
            LogHelper.UiLog.Info($"开始测试电池{ngInfo.Battery.Position}");
            int boardIndex = ngInfo.Battery.Position > 20 ? 2 : 1;
            int channelIndex = ngInfo.Battery.Position > 20 ? ngInfo.Battery.Position - 20 : ngInfo.Battery.Position;
            //var openResult = SwitchChannel(boardIndex, channelIndex);
            //if(openResult.IsFailed)
            //    return openResult;
            var openResult1 = DevicesController.SwitchBoard?.OpenChannel(boardIndex, channelIndex) ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
            if (openResult1.IsFailed) return openResult1;
            if (SettingManager.CurrentTestOption?.IsTestRes ?? false)
            {
                LogHelper.UiLog.Info("开始读取内阻");
                var resResult = DevicesController.Ohm?.ReadRes();
                if (resResult?.IsFailed ?? true)
                    return resResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}内阻失败,因为内阻仪实例为null");
                ngInfo.Battery.Res = resResult.Content;
                if(SettingManager.CurrentOcvType == Enums.OcvTypeEnmu.OCV4 && SettingManager.AcirOption is { } acirOption && (SettingManager.AcirOption?.IsAcirEnable??false))
                {
                    decimal fitFactor = 0;
                    decimal temp = (decimal)(ngInfo.Battery.PTemp ?? 0);
                    foreach(var item in acirOption.TempFits)
                    {
                        if(temp > item.MinTemp && temp<item.MaxTemp)
                        {
                            fitFactor = item.FitFactor ?? 0;
                            break;
                        }
                    }
                    ngInfo.Battery.ReserveValue1 = (double?)((decimal)(ngInfo.Battery.Res??0) + (acirOption.NominalTemp - temp) * fitFactor);
                }
            }
            var channels = new int[] { 21, 23 };
            var openResult2 = DevicesController.SwitchBoard?.OpenChannels(boardIndex, channels) ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道\"21,23\"失败，因为切换板实例为null");
            if (openResult2.IsFailed) return openResult2;
            if (SettingManager.CurrentTestOption?.IsTestVol??false)
            {
                LogHelper.UiLog.Info("开始读取电压");
                var volResult = DevicesController.Dmm?.ReadDc();
                if (volResult?.IsFailed ?? true)
                    return volResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}电压失败,因为万用表实例为null");
                ngInfo.Battery.VolValue = volResult.Content;
            }
            var closeResult = SwitchChannel(boardIndex, channelIndex, false, false);
            if (closeResult.IsFailed)
                return closeResult;
            if (SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                LogHelper.UiLog.Info("开始读取负极壳体电压");
                var openNvolChannelResult = SwitchChannel(boardIndex, channelIndex, true);
                if(openNvolChannelResult.IsFailed)
                    return openNvolChannelResult;
                var nvolResult = DevicesController.Dmm?.ReadDc();
                if(nvolResult?.IsFailed??true)
                    return nvolResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}负极壳体电压失败,因为万用表实例为null");
                ngInfo.Battery.NVolValue = Math.Abs(nvolResult.Content);
                var closeNvolChannelResult = SwitchChannel(boardIndex, channelIndex, true, false);
                if (closeNvolChannelResult.IsFailed)
                    return closeNvolChannelResult;
            }
            //if(SettingManager.CurrentTestOption?.IsTestPVol??false)
            //{
            //    if(!(SettingManager.CurrentTestOption?.IsTestVol??false)||!(SettingManager.CurrentTestOption?.IsTestNVol??false))
            //    {
            //        ngInfo.AttachedIsNg = true;
            //        ngInfo.AttachedNgDescription += " 正极壳体电压计算失败";
            //    }
            //    else
            //    {
            //        ngInfo.Battery.PVolValue = ngInfo.Battery.VolValue.sub(ngInfo.Battery.NVolValue);
            //    }
            //}

            ngInfo.Battery.IsTested = true;
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult SwitchChannel(int boardIndex, int channelIndex, bool isTestNvol = false,bool isOpen = true)
        {
            if(SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                int[] channels = new int[3];
                if (isTestNvol)
                    channels = new int[] { channelIndex, 22, 24 };
                else
                    channels = new int[] { channelIndex, 21, 23 };
                if (isOpen)
                {
                    var openResult = DevicesController.SwitchBoard?.OpenChannels(boardIndex, channels) ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                    if (openResult.IsFailed)
                        return openResult; 
                }
                else
                {
                    var closeResult = DevicesController.SwitchBoard?.CloseChannels(boardIndex, channels) ?? OperateResult.CreateFailedResult($"关闭箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                    if (closeResult.IsFailed)
                        return closeResult;
                }
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                if (isOpen)
                {
                    var openResult = DevicesController.SwitchBoard?.OpenChannel(boardIndex, channelIndex) ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                    if (openResult.IsFailed)
                        return openResult; 
                }
                else
                {
                    var closeResult = DevicesController.SwitchBoard?.CloseChannel(boardIndex, channelIndex) ?? OperateResult.CreateFailedResult($"关闭箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                    if (closeResult.IsFailed)
                        return closeResult;
                }
                return OperateResult.CreateSuccessResult();
            }
        }
    }
}
