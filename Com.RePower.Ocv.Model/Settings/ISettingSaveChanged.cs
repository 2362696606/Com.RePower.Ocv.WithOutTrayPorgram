using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Settings
{
    public interface ISettingSaveChanged
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns>保存结果</returns>
        public OperateResult SaveChanged();
        public Task<OperateResult> SaveChangedAsync();
    }
}
