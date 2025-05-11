using AutoMapper;
using Dto.ForumPosts;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.ForumPosts
{
    public class ForumPostProfile:Profile
    {
        public ForumPostProfile()
        {
            CreateMap<ForumPostDto, ForumPost>().ReverseMap();
            CreateMap<CreateForumPostDto, ForumPost>().ReverseMap();
            CreateMap<UpdateForumPostDto, ForumPost>().ReverseMap();
            CreateMap<GetForumPostDto, ForumPost>().ReverseMap();
            CreateMap<SubmitForumMatchDto, ForumPostMatchSubmitUser>().ReverseMap();
        }
    }
}
