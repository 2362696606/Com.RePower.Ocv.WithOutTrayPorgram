using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Linq;

namespace Com.RePower.Ocv.Ui.Byd.CB15.ViewModels
{
    public partial class TrayViewModel:ObservableObject
    {
        [ObservableProperty]
        private Tray _tray;

        public SettingManager SettingManager => SettingManager.Instance;


        public TrayViewModel(Tray tray)
        {
            this._tray = tray;
            _tray.PropertyChanged += AddPropChange;
        }

        private void AddPropChange(object? sender, PropertyChangedEventArgs e)
        {
            if(sender is Tray tempTray)
            {
                tempTray.NgInfos.ForEach(item => item.PropertyChanged += (s, e) => CalcTotalNg());
            }
        }

        private void CalcTotalNg()
        {
            TotalNg = _tray.NgInfos.Where(x=>x.IsNg).Count();
        }
        private int _totalNg;

        public int TotalNg
        {
            get { return _totalNg; }
            set { SetProperty(ref _totalNg, value); }
        }

    }
}
