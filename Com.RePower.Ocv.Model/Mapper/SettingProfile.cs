using AutoMapper;
using Com.RePower.Ocv.Model.Settings.Dtos;
using Com.RePower.Ocv.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<FacticitySetting,FacticitySettingDto>()
                .ReverseMap();
        }
    }
}
