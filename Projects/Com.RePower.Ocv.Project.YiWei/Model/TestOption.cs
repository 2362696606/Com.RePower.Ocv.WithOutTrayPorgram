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
    public class TestOption: Ocv.Model.Settings.TestOption,ISettingSaveChanged
    {
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
        private string _ocvType = "OCV3";
        /// <summary>
        /// Ocv类型
        /// </summary>
        [SettingName("Ocv类型")]
        public string OcvType
        {
            get { return _ocvType; }
            set { SetProperty(ref _ocvType, value); }
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
