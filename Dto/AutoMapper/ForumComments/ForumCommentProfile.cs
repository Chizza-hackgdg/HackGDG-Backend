using AutoMapper;
using Dto.ForumComments;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.ForumComments
{
    public class ForumCommentProfile: Profile
    {
        public ForumCommentProfile()
        {
            CreateMap<ForumCommentDto, ForumComment>().ReverseMap();
            CreateMap<CreateForumCommentDto, ForumComment>().ReverseMap();
            CreateMap<UpdateForumCommentDto, ForumComment>().ReverseMap();
            CreateMap<GetForumCommentDto, ForumComment>().ReverseMap();
        }
    }
}
