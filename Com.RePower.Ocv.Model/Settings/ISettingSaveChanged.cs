using Com.RePower.WpfBase;

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