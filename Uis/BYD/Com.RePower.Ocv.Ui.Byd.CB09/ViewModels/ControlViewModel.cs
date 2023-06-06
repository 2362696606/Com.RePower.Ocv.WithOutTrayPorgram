using System;
using System.Windows.Input;
using Com.RePower.Ocv.Project;
using Com.RePower.Ocv.Ui.Byd.CB09.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

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
            this.MasterTestCommand = new RelayCommand(DoMasterTest, CanDoMasterTest);
        }

        private bool CanDoMasterTest()
        {
            if (Work is Project.Byd.CB09.Works.MainWork mainWork)
            {
                //return mainWork.CanDoMasterTest();
                return true;
            }
            return false;
        }

        private void DoMasterTest()
        {
            if (Work is Project.Byd.CB09.Works.MainWork mainWork)
            {
                var view = new CalibrationSettingView
                {
                    DataContext = new CalibrationSettingViewModel(Work),
                };
                DialogHost.Show(view, "MainDialog");
            }
        }


        public RelayCommand MasterTestCommand { get; set; }

        private void DoStart()
        {
            try
            {
                if (Work?.WorkStatus == 0 || Work?.WorkStatus == 2)
                    Work.StartWorkAsync();
                else
                    Work?.PauseWorkAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
        private void DoStop()
        {
            try
            {
                CommandManager.InvalidateRequerySuggested();
                Work?.StopWorkAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
