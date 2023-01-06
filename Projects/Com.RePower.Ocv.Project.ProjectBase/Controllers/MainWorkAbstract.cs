using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.ProjectBase.Controllers
{
    public abstract partial class MainWorkAbstract : ObservableObject,IProjectMainWork
    {
        private int _workStatus;
        public MainWorkAbstract()
        {
            this.FlowController = new FlowController();
        }

        public virtual int WorkStatus
        {
            get => _workStatus;
            protected set => SetProperty(ref _workStatus, value);
        }

        public FlowController FlowController { get; }

        public async void PauseWorkAsync()
        {
            if (WorkStatus != 2)
            {
                await Task.Run(() =>
                {
                    FlowController.ResetEvent.Reset();
                    LogHelper.UiLog.Warn($"Pause");
                });
            }
            WorkStatus = 2;
        }

        public async void StartWorkAsync()
        {
            if (WorkStatus != 1)
            {
                if (WorkStatus == 0)
                {
                    WorkStatus = 1;
                    await Task.Run(() =>
                    {
                        try
                        {
                            FlowController.ResetEvent.Set();
                            var result = DoWork();
                            if (result.IsFailed)
                            {
                                LogHelper.UiLog.Warn(result.Message);
                            }
                        }
                        catch (Exception e)
                        {
                            if (e.GetType() == typeof(OperationCanceledException))
                            {
                                FlowController.CancelTokenSource = new CancellationTokenSource();
                            }
                            else
                            {
                                LogHelper.UiLog.Error(e.Message);
                            }
                        }
                        finally
                        {
                            WorkStatus = 0;
                        }
                    });
                }
                else if (WorkStatus == 2)
                {
                    WorkStatus = 1;
                    FlowController.ResetEvent.Set();
                }
            }
        }

        public async void StopWorkAsync()
        {
            if (WorkStatus != 0)
            {
                await Task.Run(() =>
                {
                    FlowController.CancelTokenSource.Cancel();
                    if (WorkStatus == 2)
                    {
                        FlowController.ResetEvent.Set();
                    }
                    LogHelper.UiLog.Warn($"Stop");
                });
            }
            WorkStatus = 0;
        }
        /// <summary>
        /// 暂停或停止
        /// </summary>
        protected void DoPauseOrStop()
        {
            #region 暂停或停止
            FlowController.ResetEvent.WaitOne();
            FlowController.CancelToken.ThrowIfCancellationRequested();
            #endregion
        }
        protected abstract OperateResult DoWork();
    }
}
