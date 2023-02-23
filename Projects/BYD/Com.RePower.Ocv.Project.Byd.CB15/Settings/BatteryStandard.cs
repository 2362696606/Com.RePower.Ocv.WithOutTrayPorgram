using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Setting
{
    public partial class BatteryStandard : Com.RePower.Ocv.Model.Settings.BatteryStandard, ISettingSaveChanged
    {
        /// <summary>
        /// 用于保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string, OperateResult>? SaveEvent { get; set; }
        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke("BatteryStandard") ?? OperateResult.CreateFailedResult("保存设置委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
