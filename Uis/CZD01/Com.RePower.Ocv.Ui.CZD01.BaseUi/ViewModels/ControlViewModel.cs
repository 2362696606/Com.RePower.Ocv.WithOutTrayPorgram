using Com.RePower.Ocv.Project;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.ViewModels
{
    public partial class ControlViewModel : ObservableObject
    {
        public ControlViewModel(IProjectMainWork? mainWork = null)
        {
            _work = mainWork;
        }


        private IProjectMainWork? _work;

        public IProjectMainWork? Work
        {
            get
            {
                if (this._work == null)
                {
                    WeakReferenceMessenger.Default.Send("未能成功获取流程实现", "MainSnackbar");
                }
                return _work;
            }
        }
        [RelayCommand]
        private void DoStart()
        {
            if (Work?.WorkStatus == 0 || Work?.WorkStatus == 2)
                Work.StartWorkAsync();
            else
                Work?.PauseWorkAsync();
        }
        [RelayCommand]
        private void DoStop()
        {
            Work?.StopWorkAsync();
        }
    }
}
