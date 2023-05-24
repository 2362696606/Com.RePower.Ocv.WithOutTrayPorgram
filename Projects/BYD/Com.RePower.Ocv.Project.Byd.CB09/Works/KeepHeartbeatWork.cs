using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works
{
    public partial class MainWork
    {
        private bool _isStartHeartbeat = false;
        private bool _currentHeartbeat = false;
        protected void KeepHeartbeat()
        {
            if (!_isStartHeartbeat)
            {
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        _currentHeartbeat = !_currentHeartbeat;
                        int value = Convert.ToInt32(_currentHeartbeat);
                        var writeResult = Plc.Write(PlcCacheSetting["Group2"]["网络心跳"].Address, (short)value);
                        if (writeResult.IsFailed)
                        {
                            LogHelper.UiLog.Warn("当前网络心跳发送失败");
                            _isStartHeartbeat = false;
                            return;
                        }
                        Thread.Sleep(1000);
                    }
                });
            }
        }
    }
}
