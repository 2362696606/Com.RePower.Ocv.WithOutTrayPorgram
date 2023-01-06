using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Mapper
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Battery, BatteryDto>();
            CreateMap<NgInfo, NgInfoDto>();
            CreateMap<Tray, TrayDto>();
        }
    }
}
