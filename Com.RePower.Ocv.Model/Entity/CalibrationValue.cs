using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Entity
{
    public class CalibrationValue:ObservableObject
    {
		private int _channel;
		/// <summary>
		/// 通道
		/// </summary>
		public int Channel
		{
			get { return _channel; }
			set { SetProperty(ref _channel, value); }
		}
		private double? _gaugeValue;
		/// <summary>
		/// 仪表值
		/// </summary>
		[JsonIgnore]
		public double? GaugeValue
		{
			get { return _gaugeValue; }
			set 
			{ 
				if(SetProperty(ref _gaugeValue, value))
				{
					OnPropertyChanged(nameof(DeviationValue));
				}
			}
		}
		private double? _standardValue;
		/// <summary>
		/// 标准值
		/// </summary>
		public double? StandardValue
		{
			get { return _standardValue; }
			set 
			{
				if(SetProperty(ref _standardValue, value))
					OnPropertyChanged(nameof(DeviationValue));
			}
        }
		/// <summary>
		/// 偏差值
		/// </summary>
		[JsonIgnore]
        public double? DeviationValue
        {
            get { return _gaugeValue - _standardValue; }
        }
        private double? _manuallyValue;
		/// <summary>
		/// 手动校准值
		/// </summary>
        public double? ManuallyValue
        {
			get { return _manuallyValue; }
			set { SetProperty(ref _manuallyValue, value); }
		}
		private double? _autoValue;
		/// <summary>
		/// 自动校准值
		/// </summary>
		public double? AutoValue
		{
			get { return _autoValue; }
			set { SetProperty(ref _autoValue, value); }
		}
	}
}
