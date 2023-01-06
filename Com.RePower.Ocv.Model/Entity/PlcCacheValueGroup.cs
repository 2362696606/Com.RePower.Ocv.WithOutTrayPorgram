using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class PlcCacheValueGroup:ObservableObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ObservableProperty]
        public string _name = "UnnamedGroup";
        /// <summary>
        /// 值列表
        /// </summary>
        [ObservableProperty]
        public List<PlcCacheValue> _plcCacheValues = new List<PlcCacheValue>();
    }
}
