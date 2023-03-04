using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Entities
{
    public class ChannelNgInfo:ObservableObject,ISettingSaveChanged
    {
        public int Channel { get; set; }
		private int _ngTimes;

		public int NgTimes
		{
			get { return _ngTimes; }
			set 
            {
                if (SetProperty(ref _ngTimes, value))
                {
                    SaveChanged();
                }
            }
		}
		public OperateResult CleanTimes()
		{
			this.NgTimes = 0;
			return OperateResult.CreateSuccessResult();
		}
        [JsonIgnore]
        public Func<OperateResult>? SaveEvent { get; set; }

        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke() ?? OperateResult.CreateFailedResult("当前保存委托为绑定实现");
        }

        public Task<OperateResult> SaveChangedAsync()
        {
            return Task.Factory.StartNew(SaveChanged);
        }
    }
}
