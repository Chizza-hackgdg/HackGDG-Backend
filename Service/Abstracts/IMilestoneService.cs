using Core.Utilities.Results;
using Dto.Milestones;
using Entity;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstracts
{
    public interface IMilestoneService:IDbOperationEvent<Milestone,Guid>
    {
        Task<IAsyncResult> CreateMilestoneAsync(CreateMilestoneDto milestoneDto);
        Task<IAsyncResult> UpdateMilestoneAsync(UpdateMilestoneDto milestoneDto);
        Task<IAsyncResult> SoftDeleteMilestoneAsync(Guid id);
        Task<IAsyncDataResult<List<GetMilestoneDto>>> GetAllMilestonesAsync();
        Task<IAsyncDataResult<GetMilestoneDto>> GetMilestoneByIdAsync(Guid id);
        Task<IAsyncDataResult<List<GetMilestoneDto>>> GetMilestonesByUserIdAsync(Guid userId);
        Task<IAsyncDataResult<List<GetMilestoneDto>>> GetMilestonesByForumPostIdAsync(Guid forumPostId);
    }
}
