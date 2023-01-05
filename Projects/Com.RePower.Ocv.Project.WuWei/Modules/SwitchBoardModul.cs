using Autofac;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Modules
{
    public class SwitchBoardModul:Module
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
                        var jObj = JObject.Parse(switchBoardSettingJson);
                        bool isReal = jObj.Value<bool>("IsReal");
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
