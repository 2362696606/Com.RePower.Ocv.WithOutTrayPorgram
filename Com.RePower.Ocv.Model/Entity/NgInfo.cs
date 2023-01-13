using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Entity
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
        private string? _ngDescription;
        /// <summary>
        /// 附加Ng描述
        /// </summary>
        [ObservableProperty]
        private string? _extraNgDescription;
        /// <summary>
        /// 是否ng true为ng，false为非ng
        /// </summary>
        [ObservableProperty]
        private bool _isNg = false;
        /// <summary>
        /// ng类型
        /// </summary>
        [ObservableProperty]
        private int? _ngType;
    }
}
