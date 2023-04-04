using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.Ocv.Project.YiWei.Decorators;
using Com.RePower.Ocv.Project.YiWei.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class PlcModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var plcSettingJStr = SettingManager<Controllers.SettingManager>.Instance.PlcSettingJson;
            if (!string.IsNullOrEmpty(plcSettingJStr))
            {
                bool isReal = SettingManager<Controllers.SettingManager>.Instance.CurrentFacticitySetting?.IsRealPlc ?? false;
                IPlc? obj;
                if (isReal)
                {
                    obj = JsonConvert.DeserializeObject<SiemensS1500Impl>(plcSettingJStr);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<PlcNetSimulator>(plcSettingJStr);
                }
                if (obj is { })
                {
                    builder.RegisterInstance(obj)
                        .AsSelf()
                        .As<IPlc>()
                        .As<IDevice>();
                }
            }
        }
    }
}
