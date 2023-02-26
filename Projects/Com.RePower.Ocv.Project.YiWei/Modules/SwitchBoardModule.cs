using Autofac;
using Com.RePower.Device.DMM.Impl;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
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
            var switchBoardSettingJStr = SettingManager<Controllers.SettingManager>.Instance.SwitchBoardSettingJson;
            if (!string.IsNullOrEmpty(switchBoardSettingJStr))
            {
                bool isReal = SettingManager<Controllers.SettingManager>.Instance.CurrentFacticitySetting?.IsRealSwitchBoard ?? false;
                ISwitchBoard? obj;
                if (isReal)
                {
                    obj = JsonConvert.DeserializeObject<FourLinesSwitchBoardImpl>(switchBoardSettingJStr);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<FourLinesSwitchBoardSimulator>(switchBoardSettingJStr);
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
