using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public partial class MonitorViewModel:ObservableObject
    {
        [ObservableProperty]
        private bool _showWaitDialog = true;
        public MonitorViewModel()
        {
            this.LocalPlcCacheValues = new ObservableCollection<PlcCacheValue>();
            //this.LogisticsPlcCacheValues = new ObservableCollection<PlcCacheValue>();
            InitCacheValues();
        }
        public ObservableCollection<PlcCacheValue> LocalPlcCacheValues { get; set; }
        //public ObservableCollection<PlcCacheValue> LogisticsPlcCacheValues{ get; set; }
        private async void InitCacheValues()
        {
            List<PlcCacheValue> tempLocal = new List<PlcCacheValue>();
            List<PlcCacheValue> tempLogistics = new List<PlcCacheValue>();
            await Task.Run(() =>
            {
                using (OcvSettingDbContext settingContext = new OcvSettingDbContext())
                {
                    var localPlcAddressCacheSettingObj = settingContext.SettingItems.First(x => x.SettingName == "本地Plc缓存");
                    if (localPlcAddressCacheSettingObj != null)
                    {
                        var localPlcAddressCacheSettingJson = localPlcAddressCacheSettingObj.JsonValue;
                        if (!string.IsNullOrEmpty(localPlcAddressCacheSettingJson))
                        {
                            JArray localPlcAddressCacheSettingArray = JArray.Parse(localPlcAddressCacheSettingJson);
                            foreach (var item in localPlcAddressCacheSettingArray)
                            {
                                var obj = item.ToObject<PlcCacheValue>();
                                if(obj!=null)
                                {
                                    tempLocal.Add(obj);
                                }
                            }
                        }
                    }

                    //var logisticsPlcAddressCacheSettingObj = settingContext.SettingItems.First(x => x.SettingName == "物流Plc缓存");
                    //if (logisticsPlcAddressCacheSettingObj != null)
                    //{
                    //    var logisticsPlcAddressCacheSettingJson = logisticsPlcAddressCacheSettingObj.JsonValue;
                    //    if (!string.IsNullOrEmpty(logisticsPlcAddressCacheSettingJson))
                    //    {
                    //        JArray logisticsPlcAddressCacheSettingArray = JArray.Parse(logisticsPlcAddressCacheSettingJson);
                    //        foreach (var item in logisticsPlcAddressCacheSettingArray)
                    //        {
                    //            var obj = item.ToObject<PlcCacheValue>();
                    //            if(obj!=null)
                    //            {
                    //                tempLogistics.Add(obj);
                    //            }
                    //        }
                    //    }
                    //}
                }
            });
            foreach(var item in tempLocal)
            {
                LocalPlcCacheValues.Add(item);
            }
            //foreach(var item in tempLogistics)
            //{
            //    LogisticsPlcCacheValues.Add(item);
            //}
            this.ShowWaitDialog = false;
        }
    }
}
