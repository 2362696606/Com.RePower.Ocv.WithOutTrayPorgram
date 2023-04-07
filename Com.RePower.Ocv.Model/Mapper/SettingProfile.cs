using AutoMapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;

namespace Com.RePower.Ocv.Model.Mapper
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<TestOption, TestOptionDto>()
                .ReverseMap();
            CreateMap<BatteryStandard, BatteryStandardSettingDto>()
                .ReverseMap();
            CreateMap<FacticitySetting, FacticitySettingDto>()
                .ReverseMap();
        }
    }
}