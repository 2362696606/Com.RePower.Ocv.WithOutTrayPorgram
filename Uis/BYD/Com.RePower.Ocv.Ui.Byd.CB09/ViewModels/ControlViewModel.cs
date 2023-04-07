using Com.RePower.Ocv.Project;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    internal class ControlViewModel:ObservableObject
    {
        public IProjectMainWork? Work { get; }
        public RelayCommand DoStartCommand { get; }
        public RelayCommand DoStopCommand { get; }

        public ControlViewModel(IProjectMainWork? work)
        {
            Work = work;
            this.DoStartCommand = new RelayCommand(DoStart);
            this.DoStopCommand = new RelayCommand(DoStop); 
        }

        private void DoStart()
        {
            if (Work?.WorkStatus == 0 || Work?.WorkStatus == 2)
                Work.StartWorkAsync();
            else
                Work?.PauseWorkAsync();
        }
        private void DoStop()
        {
            Work?.StopWorkAsync();
        }
    }
}
