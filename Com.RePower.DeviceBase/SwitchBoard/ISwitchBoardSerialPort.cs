using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.SwitchBoard
{
    /// <summary>
    /// 串口通讯切换板
    /// </summary>
    public interface ISwitchBoardSerialPort:ISwitchBoard,ISerialPortDevice
    {
    }
}
