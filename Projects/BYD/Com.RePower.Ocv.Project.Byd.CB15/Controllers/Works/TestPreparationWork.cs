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
        /// <summary>
        /// 测试准备
        /// </summary>
        /// <returns></returns>
        private OperateResult TestPreparation()
        {
            _retestCount = 0;
            _msaCount = 1;
            IsMsaTest = false;

            if (!DevicesController.Plc.IsConnected)
            {
                LogHelper.UiLog.Info("连接plc");
                var result = DevicesController.Plc.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接plc");
                }
                else
                {
                    return result;
                }
            }
            if (!(DevicesController.DMM?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接万用表");
                var result = DevicesController.DMM.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接万用表");
                }
                else
                {
                    return result;
                }
            }
            if (!(DevicesController.Ohm?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接内阻仪");
                var result = DevicesController.Ohm.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接内阻仪");
                }
                else
                {
                    return result;
                }
            }
            if (!(DevicesController.SwitchBoard?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接切换板");
                }
                else
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("测试标志位写入0");
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)0);
            if (writeResult.IsFailed)
            {
                return writeResult;
            }
            LogHelper.UiLog.Info("正在等待PLC请求测试");
            var waitResult = DevicesController.Plc.Wait(SettingManager.CurrentPlcAddressCache["请求测试"], (short)1, cancellation: FlowController.CancelToken);
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            LogHelper.UiLog.Info("完成准备流程");
            return OperateResult.CreateSuccessResult();
        }
    }
}
