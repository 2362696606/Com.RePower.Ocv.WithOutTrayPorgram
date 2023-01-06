using Com.RePower.Ocv.Model.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class PlcCacheValue:ObservableObject
    {
        [ObservableProperty]
        private string _value = string.Empty;
        public string Name { get; set; } = "Unnamed";
        public string Address { get; set; } = string.Empty;
        [JsonConverter(typeof(TypeJsonConverter))]
        public Type Type { get; set; } = typeof(short);
        public int Length { get; set; }
        public string Description { get; set; }=string.Empty;
    }
}
