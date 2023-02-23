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

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Setting
{
    public partial class TestOption : Com.RePower.Ocv.Model.Settings.TestOption,ISettingSaveChanged
    {
        /// <summary>
        /// 验证k值
        /// </summary>
        //public bool VerifyKValue { get; set; }

        private int _msaTimes;
        /// <summary>
        /// Msa测试次数
        /// </summary>
        [SettingName("Msa测试次数")]
        public int MsaTimes
        {
            get { return _msaTimes; }
            set { SetProperty(ref _msaTimes, value); }
        }
        private int _batteryCount;
        /// <summary>
        /// 托盘电池数量
        /// </summary>
        [SettingName("托盘电池数量")]
        public int BatteryCount
        {
            get { return _batteryCount; }
            set { SetProperty(ref _batteryCount, value); }
        }
        /// <summary>
        /// 用于保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string,OperateResult>? SaveEvent { get; set; }
        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke("TestOption") ?? OperateResult.CreateFailedResult("保存设置委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
