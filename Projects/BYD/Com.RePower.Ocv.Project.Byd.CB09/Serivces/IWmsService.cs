using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces
{
    public interface IWmsService
    {
        OperateResult<string> GetBatteriesInfo();
        Task<OperateResult<string>> GetBatteriesInfoAsync();
        OperateResult<string> UploadTestResult();
        Task<OperateResult<string>> UploadTestResultAsync();
    }
}
