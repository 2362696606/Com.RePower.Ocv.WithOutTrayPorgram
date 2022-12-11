using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Model.Plc
{
    public partial class CacheValue<T>:ObservableObject
    {
        /// <summary>
        /// 值
        /// </summary>
        [ObservableProperty]
        private T? value;

        public CacheValue()
        {
        }

        public CacheValue(string name, string address, string description)
        {
            Name = name;
            Address = address;
            Description = description;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "UnnamedPlcCacheValue";

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = String.Empty;

    }
}
