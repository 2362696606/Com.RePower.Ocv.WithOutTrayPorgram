using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Project.YiWei.Dto;

namespace Com.RePower.Ocv.Project.YiWei.Mapper
{
    public class YiWeiProfile:Profile
    {
        public YiWeiProfile()
        {
            //CreateMap<NgInfo,ExcelSaveDto>()
            //    .ForMember
            CreateMap<NgInfoDto, ExcelSaveDto>().IncludeMembers(x => x.Battery);
            CreateMap<BatteryDto, ExcelSaveDto>().ForMember(desc => desc.TestTime,
                opt => opt.MapFrom(x => x.TestTime.ToString("yyyy/MM/dd hh:mm:ss")));
        }
    }
}
