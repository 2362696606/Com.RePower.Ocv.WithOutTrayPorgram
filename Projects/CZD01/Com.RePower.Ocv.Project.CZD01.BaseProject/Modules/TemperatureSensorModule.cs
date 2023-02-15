using Autofac;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Device.TemperatureSensor.Impl.SerialPortTempratureSensors;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class TemperatureSensorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if((SettingManager.Instance.CurrentTestOption?.IsTestTemp??false)
                ||(SettingManager.Instance.CurrentTestOption?.IsTestNTemp??false)
                ||(SettingManager.Instance.CurrentTestOption?.IsTestPTemp??false))
            {
                var temperatureSensorSettingJStr = SettingManager.Instance.TemperatureSensorJson;
                if (!string.IsNullOrEmpty(temperatureSensorSettingJStr))
                {
                    bool isReal = SettingManager.Instance.CurrentFacticity?.IsRealTemperatureSensor ?? false;
                    ITemperatureSensor? obj;
                    if (isReal)
                    {
                        obj = JsonConvert.DeserializeObject<SerialPortTemperatureSensorImpl>(temperatureSensorSettingJStr);
                    }
                    else
                    {
                        obj = JsonConvert.DeserializeObject<SerialPortTemperatureSensorSimulator>(temperatureSensorSettingJStr);
                    }
                    if (obj is { })
                    {
                        builder.RegisterInstance(obj)
                            .AsSelf()
                            .As<ITemperatureSensor>()
                            .As<IDevice>();
                    }
                }
            }
        }
    }
}
