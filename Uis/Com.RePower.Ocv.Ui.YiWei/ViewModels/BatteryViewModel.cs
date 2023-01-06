using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public partial class BatteryViewModel:ObservableObject
    {
        [ObservableProperty]
        private NgInfo _ngInfo;
        public BatteryViewModel(NgInfo ngInfo)
        {
            _ngInfo = ngInfo;
        }
    }
}
