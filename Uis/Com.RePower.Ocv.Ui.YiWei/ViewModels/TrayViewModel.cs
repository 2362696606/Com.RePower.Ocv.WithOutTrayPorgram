using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Ui.UiBase.ViewModels;
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
            this.CurrentNgInfos = new ObservableLinkedList<NgInfo>();
            this.HistoryNgInfos = new ObservableLinkedList<NgInfo>();
            _tray = tray;
            tray.PropertyChanged += Tray_PropertyChanged;
        }

        private void Tray_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "NgInfos")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    //while (CurrentNgInfos.TryDequeue(out NgInfo dequeItem))
                    //{
                    //    if (HistoryNgInfos.Count() >= 50)
                    //        HistoryNgInfos.TryDequeue(out _);
                    //    HistoryNgInfos.Enqueue(dequeItem);
                    //}
                    //if (sender is Tray currentTray)
                    //{
                    //    foreach (var item in currentTray.NgInfos)
                    //        CurrentNgInfos.Enqueue(item);
                    //}
                    int count = CurrentNgInfos.Count;
                    for(int i = 0;i<count;i++)
                    {
                        var lastItem = CurrentNgInfos.Last();
                        if (lastItem is { })
                        {
                            CurrentNgInfos.RemoveLast();
                            if (HistoryNgInfos.Count >= 50)
                                HistoryNgInfos.RemoveLast();
                            HistoryNgInfos.AddFirst(lastItem);
                        }
                    }
                    if(sender is Tray currentTray)
                    {
                        foreach (var item in currentTray.NgInfos)
                            CurrentNgInfos.AddLast(item);
                    }
                });
            }
        }

        public ObservableLinkedList<NgInfo> CurrentNgInfos { get; }
        public ObservableLinkedList<NgInfo> HistoryNgInfos { get; }

    }
}
