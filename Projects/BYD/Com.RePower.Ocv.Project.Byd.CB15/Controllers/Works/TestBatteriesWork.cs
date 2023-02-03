﻿using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Modules;
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
        /// 测试所有电池
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult TestBatteries()
        {
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
            LogHelper.UiLog.Info($"开始测试电池{ngInfo.Battery.Position}");
            int boardIndex = ngInfo.Battery.Position > 20 ? 2 : 1;
            int channelIndex = ngInfo.Battery.Position > 20 ? ngInfo.Battery.Position - 20 : ngInfo.Battery.Position;
            var openResult = SwitchChannel(boardIndex, channelIndex);
            if(openResult.IsFailed)
                return openResult;
            if(SettingManager.CurrentTestOption?.IsTestVol??false)
            {
                var volResult = DevicesController.DMM?.ReadDc();
                if (volResult?.IsFailed ?? true)
                    return volResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}电压失败,因为万用表实例为null");
                ngInfo.Battery.VolValue = volResult.Content;
            }
            if(SettingManager.CurrentTestOption?.IsTestRes??false)
            {
                var resResult = DevicesController.Ohm?.ReadRes();
                if (resResult?.IsFailed ?? true)
                    return resResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}内阻失败,因为内阻仪实例为null");
                ngInfo.Battery.Res = resResult.Content;
            }
            if(SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                var openNvolChannelResult = SwitchChannel(boardIndex, channelIndex, true);
                if(openNvolChannelResult.IsFailed)
                    return openNvolChannelResult;
                var nvolResult = DevicesController.DMM?.ReadDc();
                if(nvolResult?.IsFailed??true)
                    return nvolResult ?? OperateResult.CreateFailedResult($"读取电池{ngInfo.Battery.Position}负极壳体电压失败,因为万用表实例为null");
                ngInfo.Battery.NVolValue = nvolResult.Content;
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult SwitchChannel(int boardIndex, int channelIndex, bool isTestNvol = false)
        {
            if(SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                int[] channels = new int[3];
                if (isTestNvol)
                    channels = new int[] { channelIndex, 21, 23 };
                else
                    channels = new int[] { channelIndex, 22, 24 };
                var openResult = DevicesController.SwitchBoard?.OpenChannels(boardIndex, channels);
                if (openResult?.IsFailed ?? true)
                    return openResult ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                var openResult = DevicesController.SwitchBoard?.OpenChannel(boardIndex, channelIndex);
                if (openResult?.IsFailed ?? true)
                    return openResult ?? OperateResult.CreateFailedResult($"切换箱号{boardIndex}的切换板通道{channelIndex}失败，因为切换板实例为null");
                return OperateResult.CreateSuccessResult();
            }
        }
    }
}
