using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model
{
    public partial class NgInfo:ObservableObject
    {
        /// <summary>
        /// 对应电池
        /// </summary>
        [ObservableProperty]
        private Battery _battery = new Battery();
        /// <summary>
        /// ng描述
        /// </summary>
        [ObservableProperty]
        private string _ngDescription = string.Empty;
        /// <summary>
        /// 是否ng
        /// </summary>
        [ObservableProperty]
        private bool _isNg = false;
        /// <summary>
        /// ng类型
        /// </summary>
        [ObservableProperty]
        private int _ngType;
    }
}
