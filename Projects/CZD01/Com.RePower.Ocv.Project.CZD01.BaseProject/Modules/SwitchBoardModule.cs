using Autofac;
using Com.RePower.Device.DMM.Impl.Keysight_34461A;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Modules
{
    public class SwitchBoardModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var switchBoardSettingJStr = SettingManager.Instance.SwitchBoardSettingJson;
            if (!string.IsNullOrEmpty(switchBoardSettingJStr))
            {
                bool isReal = SettingManager.Instance.CurrentFacticity?.IsRealSwitchBoard ?? false;
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
