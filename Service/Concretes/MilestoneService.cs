using AutoMapper;
using Core.Utilities.Results;
using Dto.Milestones;
using Entity;
using Microsoft.AspNetCore.Identity;
using Service.Abstracts;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class MilestoneService : IMilestoneService
    {
        public ITBaseService<Milestone, Guid> MilestoneCurrent { get; }
        ITBaseService<Milestone, Guid> IDbOperationEvent<Milestone, Guid>.Current => MilestoneCurrent;

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MilestoneService(ITBaseService<Milestone, Guid> milestoneCurrent, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            MilestoneCurrent = milestoneCurrent;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<Core.Utilities.Results.IAsyncResult> CreateMilestoneAsync(CreateMilestoneDto milestoneDto)
        {
            var user = await _userManager.FindByIdAsync(milestoneDto.UserId.ToString());
            if (user == null)
            {
                return new AsyncErrorResult(Task.FromResult("User not found."));
            }
            

            var milestone = _mapper.Map<Milestone>(milestoneDto);
            milestone.CreatedDate = DateTime.UtcNow;
            await MilestoneCurrent.AddAsync(milestone);
            if (user.MatchedUserId != null)
            {
                var matchedUser = await _userManager.FindByIdAsync(user.MatchedUserId.ToString());
                milestone.UserId = matchedUser.Id;
                await MilestoneCurrent.AddAsync(milestone);

            }
            return new AsyncSuccessResult(Task.FromResult("Milestone created successfully."));

        }

        public async Task<IAsyncResult> UpdateMilestoneAsync(UpdateMilestoneDto milestoneDto)
        {
            var milestone = _mapper.Map<Milestone>(milestoneDto);
            milestone.UpdatedDate = DateTime.UtcNow;
            await MilestoneCurrent.UpdateAsync(milestone);
            return new AsyncSuccessResult(Task.FromResult("Milestone updated successfully."));

        }

        public async Task<IAsyncResult> SoftDeleteMilestoneAsync(Guid id)
        {

            var milestone = await MilestoneCurrent.FirstOrDefaultAsync(x => x.Id == id);
            if (milestone == null)
            {
                return new AsyncErrorResult(Task.FromResult("Milestone not found."));
            }
            milestone.IsDeleted = true;
            milestone.DeletedDate = DateTime.UtcNow;
            await MilestoneCurrent.UpdateAsync(milestone);
            return new AsyncSuccessResult(Task.FromResult("Milestone deleted successfully."));
        }

        public async Task<IAsyncDataResult<List<GetMilestoneDto>>> GetAllMilestonesAsync()
        {

            var result = await MilestoneCurrent.GetAllListAsync();
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetMilestoneDto>>(Task.FromResult("No milestones found."));
            }
            var milestoneMap = _mapper.Map<List<GetMilestoneDto>>(result);
            return new AsyncSuccessDataResult<List<GetMilestoneDto>>(Task.FromResult(milestoneMap));
        }

        public async Task<IAsyncDataResult<GetMilestoneDto>> GetMilestoneByIdAsync(Guid id)
        {

            var result = await MilestoneCurrent.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return new AsyncErrorDataResult<GetMilestoneDto>(Task.FromResult("Milestone not found."));
            }
            var milestoneMap = _mapper.Map<GetMilestoneDto>(result);
            return new AsyncSuccessDataResult<GetMilestoneDto>(Task.FromResult(milestoneMap));
        }

        public async Task<IAsyncDataResult<List<GetMilestoneDto>>> GetMilestonesByUserIdAsync(Guid userId)
        {

            var result = await MilestoneCurrent.GetAllListAsync(x => x.UserId == userId);
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetMilestoneDto>>(Task.FromResult("No milestones found."));
            }
            var milestoneMap = _mapper.Map<List<GetMilestoneDto>>(result);
            return new AsyncSuccessDataResult<List<GetMilestoneDto>>(Task.FromResult(milestoneMap));
        }

        public async Task<IAsyncDataResult<List<GetMilestoneDto>>> GetMilestonesByForumPostIdAsync(Guid forumPostId)
        {

            var result = await MilestoneCurrent.GetAllListAsync(x => x.ForumPostId == forumPostId);
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetMilestoneDto>>(Task.FromResult("No milestones found."));
            }
            var milestoneMap = _mapper.Map<List<GetMilestoneDto>>(result);
            return new AsyncSuccessDataResult<List<GetMilestoneDto>>(Task.FromResult(milestoneMap));
        }
    }
}
