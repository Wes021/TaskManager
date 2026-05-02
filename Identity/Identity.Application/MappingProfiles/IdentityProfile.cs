using AutoMapper;
using Identity.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, UserInfoDTO>()
                .ForMember(o => o.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, JwtTokenData>()
                .ForMember(o => o.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<AddNewUserDTO, User>();
        }
    }
}
