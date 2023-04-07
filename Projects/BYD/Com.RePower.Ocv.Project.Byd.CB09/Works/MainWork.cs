using System.Configuration;
using Castle.DynamicProxy.Internal;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works
{
    public class MainWork : MainWorkAbstract
    {
        protected override OperateResult DoWork()
        {
            return OperateResult.CreateSuccessResult();
        }
    }
}