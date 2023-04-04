using Com.RePower.Ocv.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Models_Cp06
{
	public class Battery : Com.RePower.Ocv.Model.Entity.Battery
	{
		private double? _resReadValue;
		/// <summary>
		/// 内阻仪表值
		/// </summary>
		public double? ResReadValue
		{
			get { return _resReadValue; }
			set 
			{
				if(SetProperty(ref _resReadValue, value))
					OnPropertyChanged(nameof(Res));
			}
		}
		private double? _resCalibrationValue;
		/// <summary>
		/// 内阻校准值
		/// </summary>
		public double? ResCalibrationValue
		{
			get { return _resCalibrationValue; }
			set 
			{
				if (SetProperty(ref _resCalibrationValue, value))
					OnPropertyChanged(nameof(Res));
			}
		}
		public override double? Res
		{
			get 
			{
				if (_resReadValue == null) return null;
				else
				{
					return _resReadValue.Add(_resCalibrationValue ?? 0);
				}
			}
		}
	}
}
