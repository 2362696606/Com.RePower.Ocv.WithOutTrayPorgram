using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class DMMModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dmmSettingJStr = SettingManager<Controllers.SettingManager>.Instance.DmmSettingJson;
            if (!string.IsNullOrEmpty(dmmSettingJStr))
            {
                bool isReal = SettingManager<Controllers.SettingManager>.Instance.CurrentFacticitySetting?.IsRealDmm ?? false;
                IDMM? obj;
                if (isReal)
                {
                    obj = JsonConvert.DeserializeObject<Keysight_34461AImpl>(dmmSettingJStr);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<Keysight_34461ASimulator>(dmmSettingJStr);
                }
                if (obj is { })
                {
                    builder.RegisterInstance(obj)
                        .AsSelf()
                        .As<IDMM>()
                        .As<IDevice>();
                }
            }
        }
    }
}
