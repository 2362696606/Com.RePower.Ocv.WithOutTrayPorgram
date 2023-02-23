using Com.RePower.WpfBase;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        private void KeepHeartbeat()
        {
            bool status = false;
            while (true)
            {
                if(!this.DevicesController.Plc?.IsConnected??false)
                {
                    var result = DevicesController.Plc?.Connect() ?? OperateResult.CreateFailedResult("Plc为null");
                    if (result.IsFailed) PlcConnectStatus = false;
                }
                SettingManager.CurrentPlcAddressCache.TryGetValue("网络心跳", out string? address);
                if (!string.IsNullOrEmpty(address))
                {
                    short value = status ? (short)1 : (short)0;
                    var result = DevicesController.Plc?.Write(address, value) ?? OperateResult.CreateFailedResult("Plc为null");
                    if (result.IsFailed) PlcConnectStatus = false;
                    else PlcConnectStatus = true;
                }
                else PlcConnectStatus = false;
                status = !status;
                Thread.Sleep(1000);
            }
        }
    }
}
