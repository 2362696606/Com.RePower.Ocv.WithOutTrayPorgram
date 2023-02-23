using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes
{
    public class MesImpl : IMesService
    {
        public MesImpl(MesDbContext context,Tray tray,IMapper mapper)
        {
            Context = context;
            Tray = tray;
            Mapper = mapper;
        }

        public MesDbContext Context { get; }
        public SettingManager SettingManager => SettingManager.Instance;
        public Tray Tray { get; }
        public IMapper Mapper { get; }

        public OperateResult UploadResultToMes()
        {
            try
            {
                switch (SettingManager.CurrentOcvType)
                {
                    case Enums.OcvTypeEnmu.OCV1:
                        return UploadDataForOcv1();
                    case Enums.OcvTypeEnmu.OCV2:
                        return UploadDataForOcv2();
                    case Enums.OcvTypeEnmu.OCV3:
                        return UploadDataForOcv3();
                    case Enums.OcvTypeEnmu.OCV4:
                        return UploadDataForOcv4();
                    default:
                        return OperateResult.CreateFailedResult("无当前ocv工站MES上传流程");
                }
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Ocv1上传数据
        /// </summary>
        /// <returns></returns>
        public OperateResult UploadDataForOcv1()
        {
            List<Ocv1InfoDto> dtos = new List<Ocv1InfoDto>();
            foreach(var item in Tray.NgInfos)
            {
                var dto = Mapper.Map<Ocv1InfoDto>(item);
                dto.UploadTime = DateTime.Now;
                dtos.Add(dto);
            }
            Context.AddRange(dtos);
            Context.SaveChanges();
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// Ocv2上传数据
        /// </summary>
        /// <returns></returns>
        public OperateResult UploadDataForOcv2()
        {
            List<Ocv2InfoDto> dtos = new List<Ocv2InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var dto = Mapper.Map<Ocv2InfoDto>(item);
                dto.UploadTime = DateTime.Now;
                dtos.Add(dto);
            }
            Context.AddRange(dtos);
            Context.SaveChanges();
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// Ocv3上传数据
        /// </summary>
        /// <returns></returns>
        public OperateResult UploadDataForOcv3()
        {
            List<Ocv3InfoDto> dtos = new List<Ocv3InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var dto = Mapper.Map<Ocv3InfoDto>(item);
                dto.UploadTime = DateTime.Now;
                dtos.Add(dto);
            }
            Context.AddRange(dtos);
            Context.SaveChanges();
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// Ocv4上传数据
        /// </summary>
        /// <returns></returns>
        public OperateResult UploadDataForOcv4()
        {
            List<Ocv4InfoDto> dtos = new List<Ocv4InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var dto = Mapper.Map<Ocv4InfoDto>(item);
                dto.UploadTime = DateTime.Now;
                dtos.Add(dto);
            }
            Context.AddRange(dtos);
            Context.SaveChanges();
            return OperateResult.CreateSuccessResult();
        }
    }
}
