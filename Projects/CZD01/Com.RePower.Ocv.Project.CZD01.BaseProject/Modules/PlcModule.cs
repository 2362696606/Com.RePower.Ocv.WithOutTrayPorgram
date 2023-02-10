using Autofac;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Decorators;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class PlcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var plcSettingJStr = SettingManager.Instance.PlcSettingJson;
            if (!string.IsNullOrEmpty(plcSettingJStr))
            {
                bool isReal = SettingManager.Instance.CurrentFacticity?.IsRealPlc ?? false;
                IPlc? obj = null;
                if (isReal)
                {
                    obj = JsonConvert.DeserializeObject<MelsecImpl>(plcSettingJStr);
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
                builder.RegisterDecorator<PlcDecorator, IPlc>();
            }
        }
    }
}
