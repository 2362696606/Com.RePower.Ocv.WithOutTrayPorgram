using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Serivces
{
    public interface IMesService
    {
        OperateResult<string> UploadTestResult();
    }
}
