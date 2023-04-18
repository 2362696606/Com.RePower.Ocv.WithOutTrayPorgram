using Autofac;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class OhmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                var ocvTypeValue = defaultOcvType.JsonValue;
                var ocvType = Enum.Parse<OcvTypeEnmu>(ocvTypeValue);

                OcvSettingItemDto? ohmSettingObj = null;
                switch (ocvType)
                {
                    case OcvTypeEnmu.OCV1:
                        ohmSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "OhmSetting_Ocv1");
                        break;
                    case OcvTypeEnmu.OCV2:
                        ohmSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "OhmSetting_Ocv2");
                        break;
                    case OcvTypeEnmu.OCV3:
                        ohmSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "OhmSetting_Ocv3");
                        break;
                    case OcvTypeEnmu.OCV4:
                        ohmSettingObj = settingContext.SettingItems.FirstOrDefault(x => x.SettingName == "OhmSetting_Ocv4");
                        break;
                }
                if (ohmSettingObj is { })
                {
                    var ohmSettingJson = ohmSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(ohmSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealOhm ?? false;
                        IOhm? obj = null;
                        if(isReal)
                        {
                            obj = JsonConvert.DeserializeObject<HiokiBt3562Impl>(ohmSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<HiokiBt3562Simulator>(ohmSettingJson);
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
}
