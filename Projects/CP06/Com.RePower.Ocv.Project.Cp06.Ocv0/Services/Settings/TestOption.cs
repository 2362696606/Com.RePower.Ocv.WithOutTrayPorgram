using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings
{
    public partial class TestOption:Model.Settings.TestOption,ISettingSaveChanged
    {
        private int? _nVolStartChannel;

        /// <summary>
        /// 负极壳体电压起始通道
        /// </summary>
        [SettingName("负极壳体电压起始通道")]
        public int? NVolStartChannel
        {
            get { return _nVolStartChannel; }
            set { SetProperty(ref _nVolStartChannel, value); }
        }
        private int? _pVolStartChannel;
        /// <summary>
        /// 正极壳体电压起始通道
        /// </summary>
        [SettingName("正极壳体电压起始通道")]
        public int? PVolStartChannel
        {
            get { return _pVolStartChannel; }
            set { SetProperty(ref _pVolStartChannel, value); }
        }

        private int? _volChannelForOcv3;
        /// <summary>
        /// OCV3电压通道
        /// </summary>
        [SettingName("OCV3电压通道")]
        public int? VolChannelForOcv3
        {
            get { return _volChannelForOcv3; }
            set { SetProperty(ref _volChannelForOcv3, value); }
        }
        private int? _nVolChannelForOcv3;
        /// <summary>
        /// OCV3负极壳体电压通道
        /// </summary>
        [SettingName("OCV3负极壳体电压通道")]
        public int? NVolChannelForOcv3
        {
            get { return _nVolChannelForOcv3; }
            set { SetProperty(ref _nVolChannelForOcv3, value); }
        }
        private int? _volNgChannel;
        /// <summary>
        /// 电压ng通道
        /// </summary>
        [SettingName("电压ng通道")]
        public int? VolNgChannel
        {
            get { return _volNgChannel; }
            set { SetProperty(ref _volNgChannel, value); }
        }
        private int? _resNgChannel;
        /// <summary>
        /// 内阻ng通道
        /// </summary>
        [SettingName("内阻ng通道")]
        public int? ResNgChannel
        {
            get { return _resNgChannel; }
            set { SetProperty(ref _resNgChannel, value); }
        }
        private int? _kValueNgChannel;
        /// <summary>
        /// K值ng通道
        /// </summary>
        [SettingName("K值ng通道")]
        public int? KValueNgChannel
        {
            get { return _kValueNgChannel; }
            set { SetProperty(ref _kValueNgChannel, value); }
        }
        private int? _nVolNgChannel;
        /// <summary>
        /// 负极壳体电压ng通道
        /// </summary>
        [SettingName("负极壳体电压ng通道")]
        public int? NVolNgChannel
        {
            get { return _nVolNgChannel; }
            set { SetProperty(ref _nVolNgChannel, value); }
        }
        private int? _pVolNgChannel;
        /// <summary>
        /// 正极壳体电压ng通道
        /// </summary>
        [SettingName("正极壳体电压ng通道")]
        public int? PVolNgChannel
        {
            get { return _pVolNgChannel; }
            set { SetProperty(ref _pVolNgChannel, value); }
        }


        /// <summary>
        /// 实现保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string, OperateResult>? DoSaveChanged { get; set; }
        public OperateResult SaveChanged()
        {
            return DoSaveChanged?.Invoke("TestOption") ?? OperateResult.CreateFailedResult("\"TestOption\"未绑定保存实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
