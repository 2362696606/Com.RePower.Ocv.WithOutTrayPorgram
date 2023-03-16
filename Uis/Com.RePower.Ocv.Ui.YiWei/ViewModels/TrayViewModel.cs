using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Entity;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels
{
    public partial class TrayViewModel:ObservableObject
    {
        [ObservableProperty]
        private Tray _tray;
        public TrayViewModel(Tray tray)
        {
            this.CurrentNgInfos = new ObservableConcurrentQueue<NgInfo>();
            this.HistoryNgInfos = new ObservableConcurrentQueue<NgInfo>();
            _tray = tray;
            tray.PropertyChanged += Tray_PropertyChanged;
        }

        private void Tray_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "NgInfos")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    while (CurrentNgInfos.TryDequeue(out NgInfo dequeItem))
                    {
                        if (HistoryNgInfos.Count() >= 50)
                            HistoryNgInfos.TryDequeue(out _);
                        HistoryNgInfos.Enqueue(dequeItem);
                    }
                    if (sender is Tray currentTray)
                    {
                        foreach (var item in currentTray.NgInfos)
                            CurrentNgInfos.Enqueue(item);
                    }
                });
            }
        }

        public ObservableConcurrentQueue<NgInfo> CurrentNgInfos { get; }
        public ObservableConcurrentQueue<NgInfo> HistoryNgInfos { get; }

    }
}
