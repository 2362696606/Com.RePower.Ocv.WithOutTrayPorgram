using Com.RePower.Ocv.Model.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class PlcCacheValue : ObservableObject
    {
        private string? _value;

        /// <summary>
        /// 值
        /// </summary>
        [JsonIgnore]
        public string? Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "Unnamed";

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 类型
        /// </summary>
        [JsonConverter(typeof(TypeJsonConverter))]
        public Type Type { get; set; } = typeof(short);

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}