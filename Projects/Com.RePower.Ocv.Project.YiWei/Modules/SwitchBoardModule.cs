using Autofac;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.SwitchBoard.Impl;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
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
                        var obj = JsonConvert.DeserializeObject<GeneralSwitchBoardImpl>(switchBoardSettingJson);
                        //var obj = JsonConvert.DeserializeObject<GeneralSwitchBoardSimulator>(switchBoardSettingJson);
                        if (obj != null)
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
