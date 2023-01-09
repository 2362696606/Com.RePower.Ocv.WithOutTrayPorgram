using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public partial class SettingManager:ObservableObject
    {
        private WmsSetting? _wmsSettingForOcv0;
        private WmsSetting? _wmsSettingForOcv1;
        private WmsSetting? _wmsSettingForOcv2;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentWmsSetting))]
        private OcvTypeEnmu _currentOcvType;
        public SettingManager()
        {
            using (var settingContext = new OcvSettingDbContext())
            {
                var defaultOcvType = settingContext.SettingItems.First(x => x.SettingName == "DefaultOcvType");
                string defaultOcvTypeStr = defaultOcvType.JsonValue;
                this.CurrentOcvType = Enum.Parse<OcvTypeEnmu>(defaultOcvTypeStr);
                var wmsOcv0 = settingContext.SettingItems.First(x => x.SettingName == "WmsSetting_Ocv0");
                string jStrOcv0 = wmsOcv0.JsonValue;
                var wmsOcv1 = settingContext.SettingItems.First(x => x.SettingName == "WmsSetting_Ocv1");
                string jStrOcv1 = wmsOcv1.JsonValue;
                var wmsOcv2 = settingContext.SettingItems.First(x => x.SettingName == "WmsSetting_Ocv2");
                string jStrOcv2 = wmsOcv2.JsonValue;
                this._wmsSettingForOcv0 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv0);
                this._wmsSettingForOcv1 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv1);
                this._wmsSettingForOcv2 = JsonConvert.DeserializeObject<WmsSetting>(jStrOcv2);
            }
        }
        public WmsSetting? CurrentWmsSetting
        {
            get 
            {
                switch(_currentOcvType)
                {
                    case OcvTypeEnmu.OCV0:
                        return _wmsSettingForOcv0;
                    case OcvTypeEnmu.OCV1:
                        return _wmsSettingForOcv1;
                    case OcvTypeEnmu.OCV2:
                        return _wmsSettingForOcv2;
                    default:
                        return _wmsSettingForOcv0;
                }
            }
        }
    }
}
