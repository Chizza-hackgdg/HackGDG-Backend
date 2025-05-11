using AutoMapper;
using Dto.UserProfessions;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.UserProfessions
{
    public class UserProfessionProfile:Profile
    {
        public UserProfessionProfile()
        {
            CreateMap<UserProfessionDto,UserProfession>().ReverseMap();
            CreateMap<UpdateUserProfessionDto, UserProfession>().ReverseMap();
            CreateMap<CreateUserProfessionDto, UserProfession>().ReverseMap();
            CreateMap<GetUserProfessionDto, UserProfession>().ReverseMap();
            CreateMap<ForumPostMatchSubmitUser,UserProfession>().ReverseMap();
        }
    }
}
