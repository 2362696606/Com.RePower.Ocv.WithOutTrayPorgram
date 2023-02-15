using Com.RePower.Ocv.Model.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class PlcValueCacheSetting:ObservableObject
    {
		private List<PlcCacheValue> _plcCacheValues = new List<PlcCacheValue>();

		public List<PlcCacheValue> PlcAddressCache
        {
			get { return _plcCacheValues; }
			set { SetProperty(ref _plcCacheValues, value); }
		}

		public PlcCacheValue? this[string name]
		{
			get { return this.PlcAddressCache.FirstOrDefault(x => x.Name == name); }
		}
	}
}
