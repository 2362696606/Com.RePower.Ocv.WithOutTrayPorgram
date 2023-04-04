using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    public class MainWindowViewModel:ObservableObject
    {
        public MainWindowViewModel()
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknow";
        }
        public string AppVersion { get; }
    }
}
