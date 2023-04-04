using Autofac;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System.Linq;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.Device.SwitchBoard.Impl.FourLinesSwitchBoard;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class OhmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ohmSettingJStr = SettingManager<Controllers.SettingManager>.Instance.OhmSettingJson;
            if (!string.IsNullOrEmpty(ohmSettingJStr))
            {
                bool isReal = SettingManager<Controllers.SettingManager>.Instance.CurrentFacticitySetting?.IsRealOhm ?? false;
                IOhm? obj;
                if (isReal)
                {
                    obj = JsonConvert.DeserializeObject<HiokiBt3562Impl>(ohmSettingJStr);
                }
                else
                {
                    obj = JsonConvert.DeserializeObject<HiokiBt3562Simulator>(ohmSettingJStr);
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
