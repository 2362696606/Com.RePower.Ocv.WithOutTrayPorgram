using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Project.Byd.CB09.Models
{
    public class ChannelNgInfo:ObservableObject
    {
        public int Channel { get; set; }
        private int _ngTimes;
        public int NgTimes
        {
            get => _ngTimes;
            set => SetProperty(ref _ngTimes, value);
        }
    }
}
