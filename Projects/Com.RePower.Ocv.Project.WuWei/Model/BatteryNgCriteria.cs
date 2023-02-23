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
    public class BatteryNgCriteria:ObservableObject,ISettingSaveChanged
    {
        private double _maxPVol;
        /// <summary>
        /// 最大正极侧边电压
        /// </summary>
        [SettingName("最大负极壳体电压")]
        public double MaxPVol
        {
            get { return _maxPVol; }
            set { SetProperty(ref _maxPVol, value); }
        }
        private double _minPVol;
        /// <summary>
        /// 最小正极侧边电压
        /// </summary>
        [SettingName("最小负极壳体电压")]
        public double MinPVol
        {
            get { return _minPVol; }
            set { SetProperty(ref _minPVol, value); }
        }

        /// <summary>
        /// 用于保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string, OperateResult>? SaveAction { get; set; }

        public OperateResult SaveChanged()
        {
            return SaveAction?.Invoke("BatteryNgCriteria") ?? OperateResult.CreateFailedResult("保存委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
