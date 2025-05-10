using AutoMapper;
using Dto.Roles;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.Roles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleDto, AppRole>().ReverseMap();
            CreateMap<UpdateRoleDto, AppRole>().ReverseMap();
            CreateMap<ListRoleDto, AppRole>().ReverseMap();
            CreateMap<AssignRoleToUserDto, AppRole>().ReverseMap();
        }
    }
}
