using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Menu;
using EPS.Service.Dtos.Role;
using EPS.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EPS.Service.Profiles
{
    public class MenuProfileDtoToEntity : Profile
    {
        public MenuProfileDtoToEntity()
        {
            CreateMap<MenuCreateDto, Menu>();
            CreateMap<MenuUpadateDto, Menu>();
        }
    }

    public class MenuProfileEntityToDto : Profile
    {
        public MenuProfileEntityToDto()
        {
            CreateMap<Menu, MenuGridDto>()
                .ForMember(dest => dest.Created_dateStr, mo => mo.MapFrom(src => src.Created_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Updated_dateStr, mo => mo.MapFrom(src => src.Updated_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Menu, MenuDetailDto>()
                .ForMember(dest => dest.Created_dateStr, mo => mo.MapFrom(src => src.Created_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Updated_dateStr, mo => mo.MapFrom(src => src.Updated_date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
