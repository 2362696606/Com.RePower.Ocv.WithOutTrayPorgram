using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes
{
    public class MesSimulator : IMesService
    {
        public OperateResult UploadResultToMes()
        {
            return OperateResult.CreateSuccessResult();
        }
    }
}
