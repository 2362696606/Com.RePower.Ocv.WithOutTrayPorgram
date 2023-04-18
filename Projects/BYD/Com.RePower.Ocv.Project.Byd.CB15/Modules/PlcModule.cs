using Autofac;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Decorators;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class PlcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                var ocvTypeValue = defaultOcvType.JsonValue;
                var ocvType = Enum.Parse<OcvTypeEnmu>(ocvTypeValue);

                OcvSettingItemDto? localPlcSettingObj = null;
                switch (ocvType)
                {
                    case OcvTypeEnmu.OCV1:
                        localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "PlcSetting_Ocv1");
                        break;
                    case OcvTypeEnmu.OCV2:
                        localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "PlcSetting_Ocv2");
                        break;
                    case OcvTypeEnmu.OCV3:
                        localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "PlcSetting_Ocv3");
                        break;
                    case OcvTypeEnmu.OCV4:
                        localPlcSettingObj = settingContext.SettingItems.First(x => x.SettingName == "PlcSetting_Ocv4");
                        break;
                }
                if (localPlcSettingObj != null)
                {
                    var localPlcSettingJson = localPlcSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(localPlcSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealPlc ?? false;
                        IPlc? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<InovanceTcpNetPlcImpl>(localPlcSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<PlcNetSimulator>(localPlcSettingJson);
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
            builder.RegisterDecorator<PlcDecorator, IPlc>();
        }
    }
}
