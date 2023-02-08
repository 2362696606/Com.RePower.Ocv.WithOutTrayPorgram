using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes
{
    public class MesSimulator : IMesService
    {
        public OperateResult<string> BatteryDismantlingDiskDataUploadToMes()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> CapacitySortingNG()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> FormingNG()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> UploadHistoricalResult(List<NgInfoDto> ngInfos)
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> UploadingDeviceStatus(int status, bool isShutdown, string message = "")
        {
            MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
            {
                Status = true,
            };
            string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
            return OperateResult.CreateSuccessResult<string>(str);
        }

        public OperateResult<string> UploadResult()
        {
            MesBatteryResultReturnDto mesBatteryResultReturnDto = new MesBatteryResultReturnDto
            {
                Status = true,
            };
            string str = JsonConvert.SerializeObject(mesBatteryResultReturnDto);
            return OperateResult.CreateSuccessResult<string>(str);
        }

        public OperateResult<string> UserAuthentication(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> VerifyDataOCV1OCV2TestCabinet()
        {
            throw new NotImplementedException();
        }
    }
}
