namespace Com.RePower.Ocv.Project
{
    /// <summary>
    /// 项目主业务接口
    /// </summary>
    public interface IProjectMainWork:IDisposable
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public int WorkStatus { get; }
        /// <summary>
        /// 开始任务
        /// </summary>
        void StartWorkAsync();
        /// <summary>
        /// 停止任务
        /// </summary>
        void StopWorkAsync();
        /// <summary>
        /// 暂停任务
        /// </summary>
        void PauseWorkAsync();
    }
}