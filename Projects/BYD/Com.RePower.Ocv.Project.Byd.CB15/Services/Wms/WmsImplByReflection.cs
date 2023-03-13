using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsWebService;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms
{
    public class WmsImplByReflection : IWmsService
    {
        public WmsImplByReflection(Tray tray, IMapper mapper)
        {
            Tray = tray;
            Mapper = mapper;
        }


        public Tray Tray { get; }
        public SettingManager SettingManager => SettingManager.Instance;
        public IMapper Mapper { get; }

        public OperateResult<TrayInfoOCV> GetTechnologyInfoByBarCode()
        {
            throw new NotImplementedException();
        }

        public OperateResult<Result> GetTestResultByTrayCode()
        {
            throw new NotImplementedException();
        }

        public OperateResult<TrayTypeOCV> GetTrayType()
        {
            throw new NotImplementedException();
        }

        public OperateResult<Status> UpdateOCVStatus()
        {
            throw new NotImplementedException();
        }
    }
}
