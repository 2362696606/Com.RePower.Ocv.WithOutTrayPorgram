using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class PlcCacheValueGroup : ObservableObject
    {
        private string _name = "UnNamed";

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private List<PlcCacheValue> _plcCacheValues = new();

        public List<PlcCacheValue> PlcCacheValues
        {
            get => _plcCacheValues;
            set => SetProperty(ref _plcCacheValues, value);
        }
    }
}