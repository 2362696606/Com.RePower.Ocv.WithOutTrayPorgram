using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl;

public class MesSimulator:IMesService
{
    public OperateResult<string> UploadTestResult()
    {
        return OperateResult.CreateSuccessResult<string>("成功");
    }
}