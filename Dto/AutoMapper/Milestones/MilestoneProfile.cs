using AutoMapper;
using Dto.Milestones;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.AutoMapper.Milestones
{
    public class MilestoneProfile:Profile
    {
        public MilestoneProfile()
        {
            CreateMap<MilestoneDto,Milestone>().ReverseMap();
            CreateMap<CreateMilestoneDto, Milestone>().ReverseMap();
            CreateMap<UpdateMilestoneDto, Milestone>().ReverseMap();
            CreateMap<GetMilestoneDto, Milestone>().ReverseMap();
        }
    }
}
