using AutoMapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles
{
    public class SettingForCZD01Profile : Profile
    {
        public SettingForCZD01Profile()
        {
            CreateMap<TestOption, TestOptionDto>()
                .ReverseMap();
            CreateMap<BatteryStandard, BatteryStandardSettingDto>()
                .ReverseMap();
        }
    }
}
