using Autofac;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Device.TemperatureSensor.Impl.MtvTemperatureSensor;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class TempSensorModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                var ocvTypeValue = defaultOcvType.JsonValue;
                var ocvType = Enum.Parse<OcvTypeEnmu>(ocvTypeValue);

                OcvSettingItemDto? ptempSettingObj = null;
                OcvSettingItemDto? ntempSettingObj = null;
                switch (ocvType)
                {
                    case OcvTypeEnmu.OCV1:
                        ptempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "PTempSensorSetting_Ocv1");
                        ntempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "NTempSensorSetting_Ocv1");
                        break;
                    case OcvTypeEnmu.OCV2:
                        ptempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "PTempSensorSetting_Ocv2");
                        ntempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "NTempSensorSetting_Ocv2");
                        break;
                    case OcvTypeEnmu.OCV3:
                        ptempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "PTempSensorSetting_Ocv3");
                        ntempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "NTempSensorSetting_Ocv3");
                        break;
                    case OcvTypeEnmu.OCV4:
                        ptempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "PTempSensorSetting_Ocv4");
                        ntempSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "NTempSensorSetting_Ocv4");
                        break;
                }

                if (ptempSettingObj is { })
                {
                    var ptempSettingJson = ptempSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(ptempSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealPTemp ?? false;
                        ITemperatureSensor? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<MtvTemperatureSensorImpl>(ptempSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<MtvTemperatureSensorSimulator>(ptempSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .Keyed<ITemperatureSensor>("PTempSensor")
                                .As<IDevice>();
                        }
                    }
                }
                if(ntempSettingObj is { })
                {
                    var ntempSettingJson = ntempSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(ntempSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealNTemp ?? false;
                        ITemperatureSensor? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<MtvTemperatureSensorImpl>(ntempSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<MtvTemperatureSensorSimulator>(ntempSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .Keyed<ITemperatureSensor>("NTempSensor")
                                .As<IDevice>();
                        }
                    }
                }
            }
        }
    }
}
