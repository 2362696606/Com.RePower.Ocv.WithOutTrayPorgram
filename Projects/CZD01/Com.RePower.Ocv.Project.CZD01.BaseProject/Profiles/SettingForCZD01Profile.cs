using AutoMapper;
using Com.RePower.Ocv.Model.Settings;
using Com.RePower.Ocv.Model.Settings.Dtos;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Profiles
{
    public class SettingForCzd01Profile : Profile
    {
        public SettingForCzd01Profile()
        {
            CreateMap<Settings.TestOption, Settings.Dtos.TestOptionDto>()
                .ReverseMap();
            CreateMap<Settings.BatteryStandard, Settings.Dtos.BatteryStandardSettingDto>()
                .ReverseMap();
            CreateMap<WmsSetting,WmsSettingDto>()
                .ReverseMap();
            CreateMap<OtherSetting,OtherSettingDto>()
                .ReverseMap();
        }
    }
}
