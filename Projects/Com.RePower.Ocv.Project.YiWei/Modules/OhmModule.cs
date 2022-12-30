using Autofac;
using Com.RePower.Device.SwitchBoard.Impl;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.RePower.Ocv.Device.Ohm.Impl.Hioki_BT3562;
using Com.RePower.DeviceBase.Ohm;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class OhmModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
            {
                var ohmSettingObj = settingContext.SettingItems.First(x => x.SettingName == "内阻仪");
                if (ohmSettingObj != null)
                {
                    var ohmSettingJson = ohmSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(ohmSettingJson))
                    {
                        var obj = JsonConvert.DeserializeObject<Hioki_BT3562Impl>(ohmSettingJson);
                        //var obj = JsonConvert.DeserializeObject<Hioki_BT3562Simulator>(ohmSettingJson);
                        if (obj != null)
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
