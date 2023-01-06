using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;

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
