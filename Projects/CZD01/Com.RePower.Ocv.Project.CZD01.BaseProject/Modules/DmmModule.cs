using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class DmmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if ((SettingManager.Instance.CurrentTestOption?.IsTestVol ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestNVol ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestPVol ?? false)) 
            {
                var dmmSettingJStr = SettingManager.Instance.DmmSettingJson;
                if (!string.IsNullOrEmpty(dmmSettingJStr))
                {
                    bool isReal = SettingManager.Instance.CurrentFacticity?.IsRealDmm ?? false;
                    IDmm? obj;
                    if (isReal)
                    {
                        obj = JsonConvert.DeserializeObject<Keysight34461AImpl>(dmmSettingJStr);
                    }
                    else
                    {
                        obj = JsonConvert.DeserializeObject<Keysight34461ASimulator>(dmmSettingJStr);
                    }
                    if (obj is { })
                    {
                        builder.RegisterInstance(obj)
                            .AsSelf()
                            .As<IDmm>()
                            .As<IDevice>();
                    }
                } 
            }
        }
    }
}
