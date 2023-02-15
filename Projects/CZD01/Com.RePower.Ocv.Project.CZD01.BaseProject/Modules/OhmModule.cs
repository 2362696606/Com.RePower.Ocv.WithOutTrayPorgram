using Autofac;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class OhmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (SettingManager.Instance.CurrentTestOption?.IsTestRes ?? false) 
            {
                var ohmSettingJStr = SettingManager.Instance.OhmSettingJson;
                if (!string.IsNullOrEmpty(ohmSettingJStr))
                {
                    bool isReal = SettingManager.Instance.CurrentFacticity?.IsRealOhm ?? false;
                    IOhm? obj;
                    if (isReal)
                    {
                        obj = JsonConvert.DeserializeObject<Hioki_BT3562Impl>(ohmSettingJStr);
                    }
                    else
                    {
                        obj = JsonConvert.DeserializeObject<Hioki_BT3562Simulator>(ohmSettingJStr);
                    }
                    if (obj is { })
                    {
                        builder.RegisterInstance(obj)
                            .AsSelf()
                            .As<IOhm>()
                            .As<IDevice>();
                    }
                } 
            }
        }
    }
}
