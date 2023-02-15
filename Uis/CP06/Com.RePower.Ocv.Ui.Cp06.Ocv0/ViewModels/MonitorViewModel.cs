using Com.RePower.DeviceBase.Plc;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Ui.Cp06.Ocv0.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Cp06.Ocv0.ViewModels
{
    public partial class MonitorViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _showWaitDialog = true;

        //private bool _isMonitor = false;
        public MonitorViewModel(IPlc plc)
        {
            this.LocalPlcCacheValues = new ObservableCollection<PlcCacheValue>();
            InitCacheValues();
            Plc = plc;
        }
        public ObservableCollection<PlcCacheValue> LocalPlcCacheValues { get; set; }
        public IPlc Plc { get; }

        private async void InitCacheValues()
        {
            List<PlcCacheValue> tempLocal = new List<PlcCacheValue>();
            await Task.Run(() =>
            {
                using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
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
                                var obj = item.ToObject<PlcCacheValue>();
                                if (obj != null)
                                {
                                    tempLocal.Add(obj);
                                }
                            }
                        }
                    }
                }
            });
            foreach (var item in tempLocal)
            {
                LocalPlcCacheValues.Add(item);
            }
            
            this.ShowWaitDialog = false;
        }
        [RelayCommand]
        private async void StartMonitor()
        {

            await Task.Run(() =>
            {
                if (!Plc.IsConnected)
                {
                    Plc.Connect();
                }
                Plc.PlcValueMonitor(LocalPlcCacheValues);
            });
        }
    }
}
