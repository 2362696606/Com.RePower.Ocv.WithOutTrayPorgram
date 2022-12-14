namespace Com.RePower.Ocv.Project
{
    /// <summary>
    /// 项目主业务接口
    /// </summary>
    public interface IProjectMainWork
    {
        /// <summary>
        /// 任务状态0:停止,1.正在运行,2:暂停
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