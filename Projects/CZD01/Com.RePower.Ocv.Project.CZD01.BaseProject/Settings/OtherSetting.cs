using Com.RePower.Ocv.Model.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class OtherSetting:ObservableObject
    {
		private int _pVolStartChannel;
		/// <summary>
		/// 正极壳体电压开始通道
		/// </summary>
		public int PVolStartChannel
		{
			get { return _pVolStartChannel; }
			set { SetProperty(ref _pVolStartChannel, value); }
		}


		private int _nVolStartChannel;
		/// <summary>
		/// 负极壳体电压开始通道
		/// </summary>
		[SettingName("侧边电压开始通道")]
		public int NVolStartChannel
		{
			get { return _nVolStartChannel; }
			set { SetProperty(ref _nVolStartChannel, value); }
		}
		private int _volNgChannel = 3;

		/// <summary>
		/// 电压Ng通道
		/// </summary>
		[SettingName("电压Ng通道")]
        public int VolNgChannel
        {
			get { return _volNgChannel; }
			set { SetProperty(ref _volNgChannel, value); }
		}
		private int _resNgChannel = 2;
		/// <summary>
		/// 内阻Ng通道
		/// </summary>
		[SettingName("内阻Ng通道")]
        public int ResNgChannel
        {
			get { return _resNgChannel; }
			set { SetProperty(ref _resNgChannel, value); }
		}
		private int _tempNgChannel = 4;
		/// <summary>
		/// 温度Ng通道
		/// </summary>
		[SettingName("温度Ng通道")]
        public int TempNgChannel
        {
			get { return _tempNgChannel; }
			set { SetProperty(ref _tempNgChannel, value); }
		}
		private int _integralKNgChannel = 4;
		/// <summary>
		/// 整体K值Ng通道
		/// </summary>
		[SettingName("整体K值Ng通道")]
        public int IntegralKNgChannel
        {
			get { return _integralKNgChannel; }
			set { SetProperty(ref _integralKNgChannel, value); }
		}
		private int _palletKNgChannel = 4;
		/// <summary>
		/// 单托盘K值Ng通道
		/// </summary>
		[SettingName("单托盘K值Ng通道")]
        public int PalletKNgChannel
        {
			get { return _palletKNgChannel; }
			set { SetProperty(ref _palletKNgChannel, value); }
		}
		private int _volDifferenceNgChannel = 4;
		/// <summary>
		/// 压差Ng通道
		/// </summary>
		[SettingName("压差Ng通道")]
        public int VolDifferenceNgChannel
        {
			get { return _volDifferenceNgChannel; }
			set { SetProperty(ref _volDifferenceNgChannel, value); }
		}

	}
}
