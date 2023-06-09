using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult TestPreparation()
        {
            isCheckNgContinue = false;
            LogHelper.UiLog.Info("重置交互信号");
            //string address = SettingManager.PlcValueCacheSetting?["测试标志位"]?.Address ?? string.Empty;
            //if (string.IsNullOrEmpty(address))
            //    return OperateResult.CreateFailedResult("无法获取\"测试标志位\"地址");
            //var writeResult = DevicesController.Plc?.Write(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
            //if (writeResult.IsFailed)
            //{
            //    return writeResult;
            //}
            //address = SettingManager.PlcValueCacheSetting?["拆盘标志位"]?.Address ?? string.Empty;
            //if (string.IsNullOrEmpty(address))
            //    return OperateResult.CreateFailedResult("无法获取\"拆盘标志位\"地址");
            //writeResult = DevicesController.Plc?.Write(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
            //if (writeResult.IsFailed)
            //{
            //    return writeResult;
            //}
            int count = 0;
            foreach(var item in SettingManager.CurrentTestOrder??new List<List<int>>())
            {
                count += item.Count;
            }
            for (int i = 0; i < count; i++)
            {
                string address = SettingManager.PlcValueCacheSetting?[$"位置{i + 1}托盘电池状态"]?.Address ?? string.Empty;
                if (string.IsNullOrEmpty(address))
                    return OperateResult.CreateFailedResult($"无法获取\"位置{i + 1}托盘电池状态\"地址");
                var writeResult = DevicesController.Plc?.Write(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
                if (writeResult.IsFailed)
                {
                    return writeResult;
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
