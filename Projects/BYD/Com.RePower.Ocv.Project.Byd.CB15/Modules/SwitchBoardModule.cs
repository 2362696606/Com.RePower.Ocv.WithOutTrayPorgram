using Autofac;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.Ocv.Project.Byd.CB15.Enums;
using Com.RePower.Ocv.Model.Dto;

namespace Com.RePower.Ocv.Project.Byd.CB15.Modules
{
    public class SwitchBoardModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                var ocvTypeValue = defaultOcvType.JsonValue;
                var ocvType = Enum.Parse<OcvTypeEnmu>(ocvTypeValue);

                OcvSettingItemDto? switchBoardSettingObj = null;
                switch(ocvType)
                {
                    case OcvTypeEnmu.OCV1:
                        switchBoardSettingObj = settingContext.SettingItems.First(x => x.SettingName == "SwitchBoardSetting_Ocv1");
                        break;
                    case OcvTypeEnmu.OCV2:
                        switchBoardSettingObj = settingContext.SettingItems.First(x => x.SettingName == "SwitchBoardSetting_Ocv2");
                        break;
                    case OcvTypeEnmu.OCV3:
                        switchBoardSettingObj = settingContext.SettingItems.First(x => x.SettingName == "SwitchBoardSetting_Ocv3");
                        break;
                    case OcvTypeEnmu.OCV4:
                        switchBoardSettingObj = settingContext.SettingItems.First(x => x.SettingName == "SwitchBoardSetting_Ocv4");
                        break;
                }
                if (switchBoardSettingObj != null)
                {
                    var switchBoardSettingJson = switchBoardSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(switchBoardSettingJson))
                    {
                        var fObj = settingContext.SettingItems.First(x => x.SettingName == "FacticityManager");
                        FacticityManager? facticityManager = JsonConvert.DeserializeObject<FacticityManager>(fObj.JsonValue);
                        bool isReal = facticityManager?.IsRealSwitchBoard ?? false;
                        ISwitchBoard? obj = null;
                        if (isReal)
                        {
                            obj = JsonConvert.DeserializeObject<FourLinesSwitchBoardImpl>(switchBoardSettingJson);
                        }
                        else
                        {
                            obj = JsonConvert.DeserializeObject<FourLinesSwitchBoardSimulator>(switchBoardSettingJson);
                        }
                        if (obj is { })
                        {
                            builder.RegisterInstance(obj)
                                .AsSelf()
                                .As<ISwitchBoard>()
                                .As<IDevice>();
                        }
                    }
                }
            }
        }
    }
}
