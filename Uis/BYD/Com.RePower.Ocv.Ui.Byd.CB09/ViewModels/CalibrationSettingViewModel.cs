using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Project.Byd.CB09.Messages;
using Com.RePower.Ocv.Project.Byd.CB09.Models;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.Ocv.Ui.Byd.CB09.UiHelper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels;

public class CalibrationSettingViewModel:ObservableObject
{
    private readonly IProjectMainWork _work;
    private readonly CalibrationSetting _calibrationSetting;
    private List<CalibrationItemViewModel> _calibrationItems ;

    public CalibrationSettingViewModel(IProjectMainWork work)
    {
        _work = work;
        _calibrationSetting = CalibrationSetting.Default;
        this._calibrationItems = new List<CalibrationItemViewModel>();
        InitCalibrationItems();
        SaveCommand = new RelayCommand(DoSave);
        WeakReferenceMessenger.Default.Register<DoMethodMessage,string>(this, "CalibrationChangeMessage",
            MessageHelper.DoMethod);
        DoMasterTestCommand = new RelayCommand(DoMasterTest, CanDoMasterTest);
    }

    public RelayCommand DoMasterTestCommand { get; set; }

    private bool CanDoMasterTest()
    {
        if (_work is Project.Byd.CB09.Works.MainWork mainWork)
        {
            return mainWork.CanDoMasterTest();
            //return true;
        }
        return false;
    }

    private void DoMasterTest()
    {
        if (_work is Project.Byd.CB09.Works.MainWork mainWork)
        {
            try
            {
                Task.Factory.StartNew(() =>
                    {
                        mainWork.DoMasterTest();
                    }).ContinueWith((_) =>
                    {
#pragma warning disable CA1416
                        MessageQueue.Enqueue("校准完成");
#pragma warning restore CA1416
                    });
            }
            catch (Exception e)
            {
#pragma warning disable CA1416
                MessageQueue.Enqueue($"校准失败：{e.Message}");
#pragma warning restore CA1416
            }
        }
    }


    private void InitCalibrationItems()
    {
        this._calibrationItems = new List<CalibrationItemViewModel>();
        foreach (CalibrationItem item in _calibrationSetting.CalibrationItems)
        {
            _calibrationItems.Add(new CalibrationItemViewModel(item)
            {
                Channel = item.Channel,
                AutoCalibrationValue = item.AutoCalibrationValue,
                ManualCalibrationValue = item.ManualCalibrationValue,
                ReadRes = 0,
                StandRes = item.StandRes
            });
        }
    }
    public void CalibrationChange(Dictionary<string,object> parameters)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (parameters["ChangeItem"] is CalibrationChangedMessage obj)
            {
                var item = _calibrationItems.FirstOrDefault(x => x.Channel == obj.CalibrationItem.Channel);
                if (item == null)
                {
                    var addItem =
                        _calibrationSetting.CalibrationItems.First(x => x.Channel == obj.CalibrationItem.Channel);
                    _calibrationItems.Add(new CalibrationItemViewModel(addItem)
                    {
                        Channel = addItem.Channel,
                        AutoCalibrationValue = addItem.AutoCalibrationValue,
                        ManualCalibrationValue = addItem.ManualCalibrationValue,
                        ReadRes = obj.ReadResValue,
                        StandRes = addItem.StandRes
                    });
                    OnPropertyChanged(nameof(CalibrationItems));
                }
                else
                {
                    item.Channel = obj.CalibrationItem.Channel;
                    item.AutoCalibrationValue = obj.CalibrationItem.AutoCalibrationValue;
                    item.ManualCalibrationValue = obj.CalibrationItem.ManualCalibrationValue;
                    item.ReadRes = obj.ReadResValue;
                    item.StandRes = obj.CalibrationItem.StandRes;
                }
            }
        });

    }

    public List<CalibrationItemViewModel> CalibrationItems
    {
        get => _calibrationItems;
        set => SetProperty(ref _calibrationItems, value);
    }

    public bool IsUseCalibration
    {
        get => _calibrationSetting.IsUseCalibration;
        set
        {
            _calibrationSetting.IsUseCalibration = value;
            OnPropertyChanged(nameof(IsUseAutoCalibration));
        }
    }


    public bool IsUseAutoCalibration
    {
        get => _calibrationSetting.IsUseAutoCalibration;
        set
        {
            _calibrationSetting.IsUseAutoCalibration = value;
            OnPropertyChanged();
        }
    }

#pragma warning disable CA1416
    public SnackbarMessageQueue MessageQueue { get; set; } = new();
#pragma warning restore CA1416

    public RelayCommand SaveCommand { get; set; }

    private void DoSave()
    {
        _calibrationSetting.SaveChanged();
#pragma warning disable CA1416
        MessageQueue.Enqueue("保存成功");
#pragma warning restore CA1416
    }

}
public class CalibrationItemViewModel:ObservableObject
{
    private readonly CalibrationItem _item;

    public CalibrationItemViewModel(CalibrationItem item)
    {
        _item = item;
    }

    private int _channel;

    public int Channel
    {
        get => _channel;
        set
        {
            if (SetProperty(ref _channel, value))
            {
                _item.Channel = value;
            }
        }
    }

    private double _autoCalibrationValue;
    /// <summary>
    /// 自动校准值
    /// </summary>
    public double AutoCalibrationValue
    {
        get => _autoCalibrationValue;
        set
        {
            if (SetProperty(ref _autoCalibrationValue, value))
            {
                _item.AutoCalibrationValue = value;
            }
        }
    }

    private double _manualCalibrationValue;

    public double ManualCalibrationValue
    {
        get => _manualCalibrationValue;
        set
        {
            if (SetProperty(ref _manualCalibrationValue, value))
            {
                _item.ManualCalibrationValue = value;
            }
        }
    }
    private double _standRes;

    public double StandRes
    {
        get => _standRes;
        set
        {
            if (SetProperty(ref _standRes, value))
            {
                _item.StandRes = value;
            }
        }
    }
    private double _readRes;

    public double ReadRes
    {
        get => _readRes;
        set => SetProperty(ref _readRes, value);
    }
}