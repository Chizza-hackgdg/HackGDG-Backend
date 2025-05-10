using AutoMapper;
using Dto.ForumCategories;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.ForumCategories
{
    public class ForumCategoryProfile:Profile
    {
        public ForumCategoryProfile()
        {
            CreateMap<ForumCategoryDto, ForumCategory>().ReverseMap();
            CreateMap<CreateForumCategoryDto,ForumCategory>().ReverseMap();
            CreateMap<UpdateForumCategoryDto, ForumCategory>().ReverseMap();
            CreateMap<GetForumCategoryDto, ForumCategory>().ReverseMap();
        }
    }
}
