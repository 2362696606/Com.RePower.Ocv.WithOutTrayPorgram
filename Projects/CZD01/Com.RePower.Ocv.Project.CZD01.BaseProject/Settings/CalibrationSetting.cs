using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings
{
    public class CalibrationSetting : Model.Settings.CalibrationSetting, ISettingSaveChanged
    {
        /// <summary>
        /// 用于保存
        /// </summary>
        [IgnorSetting]
        [JsonIgnore]
        public Func<string, OperateResult>? SaveEvent { get; set; }
        public OperateResult SaveChanged()
        {
            return SaveEvent?.Invoke("CalibrationSetting") ?? OperateResult.CreateFailedResult("保存委托未绑定实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
