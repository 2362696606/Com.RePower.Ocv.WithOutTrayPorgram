using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Model
{
    public class BatteryNgCriteria:Com.RePower.Ocv.Model.Settings.BatteryStandard,ISettingSaveChanged
    {
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
