using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Mes
{
    public class MesDtoProfile:Profile
    {
        public MesDtoProfile()
        {
            CreateMap<NgInfo, OcvInfoDtoBase>()
                .ForMember(desc => desc.Batcode, opt => opt.MapFrom(x => x.Battery.BarCode))
                .ForMember(desc => desc.WorkedTime, opt => opt.MapFrom(x => x.Battery.TestTime))
                .ForMember(desc => desc.TrayCode, opt => opt.MapFrom(x => x.Battery.TrayCode))
                .ForMember(desc => desc.BatChannel, opt => opt.MapFrom(x => x.Battery.Position))
                .ForMember(desc => desc.BatTemp, opt => opt.MapFrom(x => x.Battery.PTemp))
                .ForMember(desc=>desc.NTSVResult,opt=>opt.MapFrom(x=>x.Battery.NVolValue))
                .ForMember(desc=>desc.PTSVResult,opt=>opt.MapFrom(x=>x.Battery.PVolValue))
                .ForMember(desc => desc.NGCode, opt => opt.MapFrom(x => x.IsNg ? "Ng" : "OK"));
            CreateMap<NgInfo, Ocv1InfoDto>()
                .IncludeBase<NgInfo, OcvInfoDtoBase>()
                .ForMember(desc => desc.OCV1, opt => opt.MapFrom(x => x.Battery.VolValue));

            CreateMap<NgInfo, Ocv2InfoDto>()
                .IncludeBase<NgInfo, OcvInfoDtoBase>()
                .ForMember(desc => desc.OCV2, opt => opt.MapFrom(x => x.Battery.VolValue));

            CreateMap<NgInfo, Ocv3InfoDto>()
                .IncludeBase<NgInfo, OcvInfoDtoBase>()
                .ForMember(desc => desc.OCV3, opt => opt.MapFrom(x => x.Battery.VolValue));

            CreateMap<NgInfo, Ocv4InfoDto>()
                .IncludeBase<NgInfo, OcvInfoDtoBase>()
                //.ForMember(desc => desc.ACIR_RO, opt => opt.MapFrom(x => x.Battery.Res))
                .ForMember(desc => desc.OCV4_V1, opt => opt.MapFrom(x => x.Battery.VolValue))
                .ForMember(desc => desc.OCV4_V2, opt => opt.MapFrom(x => x.Battery.NVolValue))
                .ForMember(desc => desc.ACIR_RO, opt => opt.MapFrom(x => x.Battery.Res))
                .ForMember(desc => desc.ACIR_R, opt => opt.MapFrom(x => x.Battery.ReserveValue1));

            CreateMap<NgInfo, OcvInfoForCvsDtoBase>()
                .ForMember(desc => desc.BarCode, opt => opt.MapFrom(x => x.Battery.BarCode))
                .ForMember(desc => desc.Index, opt => opt.MapFrom(x => x.Battery.Position))
                .ForMember(desc => desc.UploadTime, opt => opt.MapFrom(x => x.Battery.TestTime))
                .ForMember(desc => desc.EndTime, opt => opt.MapFrom(x => x.Battery.TestTime))
                .ForMember(desc => desc.TrayCode, opt => opt.MapFrom(x => x.Battery.TrayCode))
                .ForMember(desc => desc.Position, opt => opt.MapFrom(x => x.Battery.Position))
                .ForMember(desc => desc.NVol, opt => opt.MapFrom(x => x.Battery.NVolValue))
                .ForMember(desc => desc.PVol, opt => opt.MapFrom(x => x.Battery.PVolValue))
                .ForMember(desc => desc.Temp, opt => opt.MapFrom(x => x.Battery.PTemp))
                .ForMember(desc => desc.EndCode, opt => opt.MapFrom(x => x.IsNg ? "Ng" : "Ok"));
            //CreateMap<NgInfo, OcvInfoForCvsDtoBase>().ConvertUsing<NgInfoToOcvInfoForCvsConverter>();
            CreateMap<NgInfo, Ocv1InfoForCvsDto>()
                .IncludeBase<NgInfo, OcvInfoForCvsDtoBase>()
                .ForMember(desc => desc.Vol, opt => opt.MapFrom(x => x.Battery.VolValue));
            CreateMap<NgInfo, Ocv2InfoForCvsDto>()
                .IncludeBase<NgInfo, OcvInfoForCvsDtoBase>()
                .ForMember(desc => desc.Vol, opt => opt.MapFrom(x => x.Battery.VolValue));
            CreateMap<NgInfo, Ocv3InfoForCvsDto>()
                .IncludeBase<NgInfo, OcvInfoForCvsDtoBase>()
                .ForMember(desc => desc.Vol, opt => opt.MapFrom(x => x.Battery.VolValue));
            CreateMap<NgInfo, Ocv4InfoForCvsDto>()
                .IncludeBase<NgInfo, OcvInfoForCvsDtoBase>()
                .ForMember(desc => desc.Vol, opt => opt.MapFrom(x => x.Battery.VolValue))
                .ForMember(desc => desc.AcirOriginal, opt => opt.MapFrom(x => x.Battery.Res))
                .ForMember(desc => desc.AcirFit, opt => opt.MapFrom(x => x.Battery.ReserveValue1));
        }


    }
    public class NgInfoToOcvInfoForCvsConverter : ITypeConverter<NgInfo, OcvInfoForCvsDtoBase>
    {
        public OcvInfoForCvsDtoBase Convert(NgInfo source, OcvInfoForCvsDtoBase destination, ResolutionContext context)
        {
            destination.BarCode = source.Battery.BarCode;
            destination.Index = source.Battery.Position;
            destination.UploadTime = source.Battery.TestTime;
            destination.EndTime = DateTime.Now;
            destination.TrayCode = source.Battery.TrayCode??string.Empty;
            destination.Position = source.Battery.Position;
            destination.NVol = source.Battery.NVolValue;
            destination.PVol = source.Battery.PVolValue;
            destination.Temp = source.Battery.PTemp;
            destination.EndCode = source.IsNg ? "Ng" : "Ok";
            destination.PcCode = SettingManager.Instance.CurrentMesSetting?.PcId??string.Empty;
            return destination;
        }
    }
}
