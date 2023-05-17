using System.Windows.Controls;
using System.Windows.Documents;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.Ocv.Project.YiWei.Controllers;
using Com.RePower.Ocv.Project.YiWei.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace Com.RePower.Ocv.Ui.YiWei.ViewModels;

public class ConfigSelectViewModel:ObservableObject
{
    public ConfigSelectViewModel()
    {
        DoSelectedCommand = new RelayCommand<UserControl>(DoSelected);
        DoCloseCommand = new RelayCommand<UserControl>(DoClose);
        SelectedConfig = SettingManager<SettingManager>.Instance.CurrentConfigSelectedEnum;
    }
    private ConfigSelectedEnum _selectedConfig;
    public ConfigSelectedEnum SelectedConfig
    {
        get => _selectedConfig;
        set => SetProperty(ref _selectedConfig, value);
    }
    
    public RelayCommand<UserControl> DoSelectedCommand { get; set; }
    public RelayCommand<UserControl> DoCloseCommand { get; set; }

    private void DoSelected(UserControl? control)
    {
        SettingManager<SettingManager>.Instance.CurrentConfigSelectedEnum = SelectedConfig;
        CloseDialog(control);
    }

    private void DoClose(UserControl? control)
    {
        CloseDialog(control);
    }
    private void CloseDialog(UserControl? control)
    {
        var command = DialogHost.CloseDialogCommand;
        if (command.CanExecute(null, control))
        {
            command.Execute(null, control);
        }
    }
}