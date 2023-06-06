using System.Configuration;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using MaterialDesignThemes.Wpf;
using TabControl = System.Windows.Controls.TabControl;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels;

public class SettingsViewModel:ObservableObject
{
    public SettingsViewModel()
    {
        this.DoSaveCommand = new RelayCommand<object>(DoSave);
        this.DoUpgradeCommand = new RelayCommand<object>(DoUpdate);
    }

    public TestOption TestOption=>TestOption.Default;
    public BatteryStandard BatteryStandard => BatteryStandard.Default;
    public OtherSetting OtherSetting => OtherSetting.Default;
    public RelayCommand<object> DoSaveCommand { get; }
    public RelayCommand<object> DoUpgradeCommand { get; }
    public SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();
    private void DoSave(object? obj)
    {
        if (obj is TabControl { SelectedContent: PropertyGrid { SelectedObject: ApplicationSettingsBase setting } })
        {
            setting.Save();
            MessageQueue.Enqueue("已保存");
        }
    }

    private void DoUpdate(object? obj)
    {
        if (obj is TabControl { SelectedContent: PropertyGrid { SelectedObject: ApplicationSettingsBase setting } })
        {
            setting.Upgrade();
            MessageQueue.Enqueue("更新成功");
        }
    }
}