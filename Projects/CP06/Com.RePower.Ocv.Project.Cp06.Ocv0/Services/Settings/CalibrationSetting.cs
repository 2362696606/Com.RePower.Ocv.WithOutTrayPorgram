using Com.RePower.Ocv.Model.Attributes;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings
{
    public class CalibrationSetting : Model.Settings.CalibrationSetting, ISettingSaveChanged
    {
        /// <summary>
        /// 实现保存
        /// </summary>
        [JsonIgnore]
        [IgnorSetting]
        public Func<string,OperateResult>? DoSaveChanged { get; set; }
        public OperateResult SaveChanged()
        {
            return DoSaveChanged?.Invoke("Calibration") ?? OperateResult.CreateFailedResult("\"Calibration\"未绑定保存实现");
        }

        public async Task<OperateResult> SaveChangedAsync()
        {
            return await Task.Run(SaveChanged);
        }
    }
}
