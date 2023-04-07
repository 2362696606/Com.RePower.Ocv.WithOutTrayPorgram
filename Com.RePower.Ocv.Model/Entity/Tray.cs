using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class Tray : ObservableObject
    {
        /// <summary>
        /// 托盘条码
        /// </summary>
        [ObservableProperty]
        private string _trayCode = string.Empty;

        /// <summary>
        /// 任务号
        /// </summary>
        [ObservableProperty]
        private long? _taskCode;

        /// <summary>
        /// Ng结果
        /// </summary>
        [ObservableProperty]
        private List<NgInfo> _ngInfos = new List<NgInfo>();
    }
}