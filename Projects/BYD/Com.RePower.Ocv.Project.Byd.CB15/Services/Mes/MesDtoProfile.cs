using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
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
            CreateMap<NgInfo,OcvInfoDtoBase>()
                .ForMember(desc => desc.Batcode, opt => opt.MapFrom(x => x.Battery.BarCode))
                .ForMember(desc => desc.WorkedTime, opt => opt.MapFrom(x => x.Battery.TestTime))
                .ForMember(desc => desc.TrayCode, opt => opt.MapFrom(x => x.Battery.TrayCode))
                .ForMember(desc => desc.BatChannel, opt => opt.MapFrom(x => x.Battery.Position))
                .ForMember(desc => desc.BatTemp, opt => opt.MapFrom(x => x.Battery.PTemp))
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
                .ForMember(desc => desc.OCV4_V2, opt => opt.MapFrom(x => x.Battery.NVolValue));
        }
    }
}
