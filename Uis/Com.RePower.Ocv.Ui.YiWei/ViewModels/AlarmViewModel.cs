using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.YiWei.Exitnsions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public class AlarmViewModel:ObservableObject
    {
        public AlarmViewModel(IPlc plc)
        {
            PlcCacheItems = new List<PlcCacheValue>();
            var jsonValue = SettingManager.PlcAlarmCacheJsonValue;
            if (!string.IsNullOrEmpty(jsonValue))
            {
                var items = JsonConvert.DeserializeObject<List<PlcCacheValue>>(jsonValue);
                if (items is { } && items.Count > 0)
                {
                    PlcCacheItems = items;
                    Task.Factory.StartNew(StartMonitor);
                }
            }
            Plc = plc;
        }

        private void StartMonitor()
        {
            while (true)
            {
                foreach (var item in PlcCacheItems)
                {
                    Plc.ReadValue(item);
                }
                Thread.Sleep(500);
            }
        }

        public Project.YiWei.Controllers.SettingManager SettingManager => Project.ProjectBase.Controllers.SettingManager<Project.YiWei.Controllers.SettingManager>.Instance;
        public List<PlcCacheValue> PlcCacheItems { get; }
        public IPlc Plc { get; }
    }
}
