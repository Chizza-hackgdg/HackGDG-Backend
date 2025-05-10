using AutoMapper;
using Dto.ApplicationUsers;
using Dto.Roles;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.ApplicationUsers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, ApplicationUser>().ReverseMap();
            CreateMap<ListUserDto, ApplicationUser>().ReverseMap();
            CreateMap<EditUserDto, ApplicationUser>().ReverseMap();
        }
    }
}
