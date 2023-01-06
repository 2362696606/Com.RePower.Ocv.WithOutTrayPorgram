using Autofac;
using Com.RePower.DeviceBase;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json;
using System.Linq;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;

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
