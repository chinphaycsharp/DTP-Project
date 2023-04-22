using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Footer;
using EPS.Service.Dtos.Menu;
using System.Globalization;

namespace EPS.Service.Profiles
{
    public class FooterProfileDtoToEntity : Profile
    {
        public FooterProfileDtoToEntity()
        {
            CreateMap<MenuCreateDto, Menu>();
            CreateMap<MenuUpadateDto, Menu>();
        }
    }

    public class FooterProfileEntityToDto : Profile
    {
        public FooterProfileEntityToDto()
        {
            CreateMap<Menu, FooterGridDto>()
                .ForMember(dest => dest.Created_dateStr, mo => mo.MapFrom(src => src.Created_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Updated_dateStr, mo => mo.MapFrom(src => src.Updated_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Menu, FooterDetailDto>()
                .ForMember(dest => dest.Created_dateStr, mo => mo.MapFrom(src => src.Created_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Updated_dateStr, mo => mo.MapFrom(src => src.Updated_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
