using Autofac;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Modules
{
    public class SwitchBoardModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var switchBoardSettingObj = settingContext.SettingItems.First(x => x.SettingName == "切换板");
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
