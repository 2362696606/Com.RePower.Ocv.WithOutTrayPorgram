using Com.RePower.Ocv.Model.Helper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.WuWei.ViewModels
{
    public partial class UiLogViewModel:ObservableObject
    {
        public static ObservableCollection<LoggingEvent> LogSource = new ObservableCollection<LoggingEvent>();

        [RelayCommand]
        private void LogDoubleClick()
        {
            if (OperatingSystem.IsWindows())
            {
                string path = $@".\Log\{DateTime.Now:yyyy-MM-dd}\{DateTime.Now:yyMMdd}_Msg_UiLog.log";
                path = Path.GetFullPath(path);
                if (File.Exists(path))
                {
                    var count = File.ReadLines(path).Count();
                    var isTheSoft = WindowSoftHelper.IsTheSoft("notepad++.exe");
                    if (isTheSoft)
                    {
                        path = $" -n{count} " + path;
                        //System.Diagnostics.Process.Start("notepad++.exe", path);
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("notepad++.exe", path) { UseShellExecute = true });
                    }
                    else
                    {
                        System.Diagnostics.Process.Start("notepad.exe", path);
                    }
                }
            }
        }
    }
}
