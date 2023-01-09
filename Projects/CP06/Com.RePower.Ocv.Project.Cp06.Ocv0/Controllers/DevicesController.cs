using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public class DevicesController
    {
        public DevicesController(IPlc localPlc
            , IDMM dMM
            , IOhm ohm
            , ISwitchBoard switchBoard)
        {
            Plc = localPlc;
            DMM = dMM;
            Ohm = ohm;
            SwitchBoard = switchBoard;
            this.PlcAddressCache = new Dictionary<string, string>();
            using (var settingContext = new OcvSettingDbContext())
            {
                var localPlcAddressCacheSettingObj = settingContext.SettingItems.First(x => x.SettingName == "Plc缓存");
                if (localPlcAddressCacheSettingObj != null)
                {
                    var localPlcAddressCacheSettingJson = localPlcAddressCacheSettingObj.JsonValue;
                    if (!string.IsNullOrEmpty(localPlcAddressCacheSettingJson))
                    {
                        JArray localPlcAddressCacheSettingArray = JArray.Parse(localPlcAddressCacheSettingJson);
                        foreach (var item in localPlcAddressCacheSettingArray)
                        {
                            var name = item.Value<string>("Name");
                            var address = item.Value<string>("Address");
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
                            {
                                PlcAddressCache.Add(name, address);
                            }
                        }
                    }
                }
            }
            //this.LogisticsPlcAddressCache = new Dictionary<string, string>();
        }

        public IPlc Plc { get; }
        public IDMM DMM { get; }
        public IOhm Ohm { get; }
        public ISwitchBoard SwitchBoard { get; }
        public Dictionary<string, string> PlcAddressCache { get; set; }
    }
}
