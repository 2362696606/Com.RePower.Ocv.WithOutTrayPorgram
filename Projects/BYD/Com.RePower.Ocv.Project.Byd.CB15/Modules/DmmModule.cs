using Autofac;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class DmmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                var ocvTypeValue = defaultOcvType.JsonValue;
                var ocvType = Enum.Parse<OcvTypeEnmu>(ocvTypeValue);

                OcvSettingItemDto? dmmSettingObj = null;
                switch (ocvType)
                {
                    case OcvTypeEnmu.OCV1:
                        dmmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "DmmSetting_Ocv1");
                        break;
                    case OcvTypeEnmu.OCV2:
                        dmmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "DmmSetting_Ocv2");
                        break;
                    case OcvTypeEnmu.OCV3:
                        dmmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "DmmSetting_Ocv3");
                        break;
                    case OcvTypeEnmu.OCV4:
                        dmmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "DmmSetting_Ocv4");
                        break;
                }

                if (dmmSettingObj != null)
                {
                    var dmmSettingJson = dmmSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(dmmSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealDmm ?? false;
                        IDmm? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<Keysight34461AImpl>(dmmSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<Keysight34461ASimulator>(dmmSettingJson);
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
}
