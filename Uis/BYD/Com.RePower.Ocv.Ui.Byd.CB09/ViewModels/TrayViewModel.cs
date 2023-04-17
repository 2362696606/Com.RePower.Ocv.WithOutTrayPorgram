using Com.RePower.Ocv.Model.Entity;
using System.ComponentModel;
using System.Linq;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    class TrayViewModel:ObservableObject
    {
        public TrayViewModel(Tray tray)
        {
            this._tray = tray;
            _tray.PropertyChanged += AddPropChange;
        }

        private void AddPropChange(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Tray tempTray)
            {
                tempTray.NgInfos.ForEach(item => item.PropertyChanged += (_, _) => CalcTotalNg());
            }
        }

        private void CalcTotalNg()
        {
            TotalNg = _tray.NgInfos.Count(x => x.IsNg);
        }
        private int _totalNg;
        private readonly Tray _tray;

        public int TotalNg
        {
            get => _totalNg;
            set => SetProperty(ref _totalNg, value);
        }

        public Tray Tray => _tray;

        public TestOption SettingManager => new TestOption();
    }
}
