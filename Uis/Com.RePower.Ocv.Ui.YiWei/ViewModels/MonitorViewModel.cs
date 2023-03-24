using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
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
using System.Windows;
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
            InitCacheValues();
        }
        public Project.YiWei.Controllers.SettingManager SettingManager => Project.ProjectBase.Controllers.SettingManager<Project.YiWei.Controllers.SettingManager>.Instance;

        public ObservableCollection<PlcCacheValue> LocalPlcCacheValues { get; set; }
        private void InitCacheValues()
        {
            List<PlcCacheValue> tempLocal = new List<PlcCacheValue>();
            List<PlcCacheValue> tempLogistics = new List<PlcCacheValue>();
            Task.Factory.StartNew(() =>
            {
                var localPlcAddressCacheSettingJson = SettingManager.PlcCacheJsonValue;
                if (!string.IsNullOrEmpty(localPlcAddressCacheSettingJson))
                {
                    JArray localPlcAddressCacheSettingArray = JArray.Parse(localPlcAddressCacheSettingJson);
                    foreach (var item in localPlcAddressCacheSettingArray)
                    {
                        var obj = item.ToObject<PlcCacheValue>();
                        if (obj != null)
                        {
                            Application.Current.Dispatcher.Invoke(() => { LocalPlcCacheValues.Add(obj); });
                            //tempLocal.Add(obj);
                        }
                    }
                }
            });
            this.ShowWaitDialog = false;
        }
    }
}
