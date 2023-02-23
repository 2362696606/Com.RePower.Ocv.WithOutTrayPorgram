using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Model
{
    public class TestOption : ObservableObject, ISettingSaveChanged
    {
        private bool _isDoRetest;
        /// <summary>
        /// 是否复测
        /// </summary>
        [SettingName("是否复测")]
        public bool IsDoRetest
        {
            get { return _isDoRetest; }
            set { SetProperty(ref _isDoRetest, value); }
        }
        private int _retestTimes;
        /// <summary>
        /// 复测次数
        /// </summary>
        [SettingName("复测次数")]
        public int RetestTimes
        {
            get { return _retestTimes; }
            set { SetProperty(ref _retestTimes, value); }
        }
        private bool _isDoUploadToMes;
        /// <summary>
        /// 是否上传结果到mes
        /// </summary>
        [SettingName("是否上传结果到mes")]
        public bool IsDoUploadToMes
        {
            get { return _isDoUploadToMes; }
            set { SetProperty(ref _isDoUploadToMes, value); }
        }
        private string _ocvType = "Ocv3";
        /// <summary>
        /// Ocv类型
        /// </summary>
        [IgnorSetting]
        [SettingName("Ocv类型")]
        public string OcvType
        {
            get { return _ocvType; }
            set { SetProperty(ref _ocvType, value); }
        }
        private int _switchDelay;
        /// <summary>
        /// 切换版切换延迟
        /// </summary>
        [IgnorSetting]
        [SettingName("切换版切换延迟")]
        public int SwitchDelay
        {
            get { return _switchDelay; }
            set { SetProperty(ref _switchDelay, value); }
        }
        private int _dMMReadDelayWhenSwitch;
        /// <summary>
        /// 万用表在切换通道后延迟读取
        /// </summary>
        [IgnorSetting]
        [SettingName("万用表在切换通道后延迟读取")]
        public int DMMReadDelayWhenSwitch
        {
            get { return _dMMReadDelayWhenSwitch; }
            set { SetProperty(ref _dMMReadDelayWhenSwitch, value); }
        }
        private int _msaTimes;
        /// <summary>
        /// Msa执行次数
        /// </summary>
        [SettingName("Msa执行次数")]
        public int MsaTimes
        {
            get { return _msaTimes; }
            set { SetProperty(ref _msaTimes, value); }
        }
        /// <summary>
        /// 用于保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string, OperateResult>? SaveAction { get; set; }

        public OperateResult SaveChanged()
        {
            return SaveAction?.Invoke("TestOption") ?? OperateResult.CreateFailedResult("保存委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
