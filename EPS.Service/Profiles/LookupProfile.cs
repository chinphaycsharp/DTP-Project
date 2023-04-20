using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using System.Linq;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.Privilege;
using System.Globalization;

namespace EPS.Service.Profiles
{
    public class LookupProfileDtoToEntity : Profile
    {
        public LookupProfileDtoToEntity()
        {
            CreateMap<PrivilegeCreateDto, Privilege>();
            CreateMap<PrivilegeUpdateDto,Privilege>();
        }
    }

    public class LookupProfileEntityToDto : Profile
    {
        public LookupProfileEntityToDto()
        {
            CreateMap<Privilege, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Role, SelectItem>()
                .ForMember(dest => dest.Text, mo => mo.MapFrom(src => src.Name));

            CreateMap<Unit, UnitTreeDto>();
        }
    }
}
