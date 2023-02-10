using Com.RePower.WpfBase;
using FluentFTP;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        [Obsolete]
        private OperateResult UploadToFtp()
        {
            var ftpClient = new FtpClient(SettingManager.CurrentMesSetting?.FtpService ?? null);
            ftpClient.Credentials = new NetworkCredential(SettingManager.CurrentMesSetting?.FtpService ?? null, SettingManager.CurrentMesSetting?.FtpService ?? null);
            ftpClient.Connect();
            var time = DateTime.Now;
            string dayStr = time.ToString("yyyy_MM_dd");
            //string timeStr = time.ToString("HH_mm");
            string directoryStr = $"OCV/{SettingManager.CurrentOcvType}/{dayStr}";
            var localDir = @$"./测试记录/{dayStr}";
            var files = Directory.GetFiles(localDir);
            foreach(var file in files)
            {

            }

            ftpClient.CreateDirectory(directoryStr);

            return OperateResult.CreateSuccessResult();
        }
    }
}
