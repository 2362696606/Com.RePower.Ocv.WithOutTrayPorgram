using Com.RePower.Ocv.Project.WuWei.Controllers;
using Com.RePower.Ocv.Project.WuWei.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.WuWei.ViewModels
{
    public class SettingViewModel:ObservableObject
    {
        public BatteryNgCriteria? BatteryNgCriteria => SettingManager.Instance.BatteryNgCriteria;
        public TestOption? TestOption => SettingManager.Instance.TestOption;
    }
}
