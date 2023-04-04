using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Helper;
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
        private IDmm? _dmm;
        private IOhm? _ohm;
        private ISwitchBoard? _switchBoard;
        public DevicesController(IPlc localPlc
            , IDmm? dMm
            , IOhm? ohm
            , ISwitchBoard? switchBoard)
        {
            Plc = localPlc;
            _dmm = dMm;
            _ohm = ohm;
            _switchBoard = switchBoard;
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

        public IDmm? Dmm
        {
            get 
            {
                //if (_dmm == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _dmm; 
            }
        }

        public IOhm? Ohm
        {
            get 
            {
                //if (_ohm == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _ohm; 
            }
        }

        public ISwitchBoard? SwitchBoard
        {
            get 
            {
                //if(_switchBoard == null)
                //{
                //    LogHelper.UiLog.Error("万用表为null");
                //}
                return _switchBoard; 
            }
        }


        public Dictionary<string, string> PlcAddressCache { get; set; }
    }
}
