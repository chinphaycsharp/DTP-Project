using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Menu;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EPS.Service.Profiles
{
    public class NewProfileDtoToEntity : Profile
    {
        public NewProfileDtoToEntity()
        {
            CreateMap<MenuCreateDto, Menu>();
            CreateMap<MenuUpadateDto, Menu>();
        }
    }

    public class NewProfileEntityToDto : Profile
    {
        public NewProfileEntityToDto()
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
