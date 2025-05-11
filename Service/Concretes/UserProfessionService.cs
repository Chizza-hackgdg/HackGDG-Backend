using AutoMapper;
using Core.Utilities.Results;
using Dto.UserProfessions;
using Entity;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class UserProfessionService : IUserProfessionService
    {
        public ITBaseService<UserProfession, Guid> UserProfessionCurrent { get; }

        ITBaseService<UserProfession,Guid> IDbOperationEvent<UserProfession,Guid>.Current => UserProfessionCurrent;
        private readonly IMapper _mapper;

        public UserProfessionService(IMapper mapper, ITBaseService<UserProfession, Guid> userProfessionCurrent)
        {
            _mapper = mapper;
            UserProfessionCurrent = userProfessionCurrent;
        }

        public async Task<Core.Utilities.Results.IAsyncResult> CreateUserProfessionAsync(CreateUserProfessionDto professionDto)
        {
            var userProfession = _mapper.Map<UserProfession>(professionDto);
            userProfession.CreatedDate = DateTime.UtcNow;
            userProfession.IsActive = true;
            await UserProfessionCurrent.AddAsync(userProfession);
            return new AsyncSuccessResult(Task.FromResult("User profession added succesfully."));
        }

        public async Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetAllUserProfessionsAsync()
        {
            var result = await UserProfessionCurrent.GetAllListAsync();
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetUserProfessionDto>>(Task.FromResult("No user professions found."));
            }
            var professionMap = _mapper.Map<List<GetUserProfessionDto>>(result);
            return new AsyncSuccessDataResult<List<GetUserProfessionDto>>(Task.FromResult(professionMap));

        }

        public async Task<IAsyncDataResult<GetUserProfessionDto>> GetUserProfessionByIdAsync(Guid id)
        {
            var result = await UserProfessionCurrent.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return new AsyncErrorDataResult<GetUserProfessionDto>(Task.FromResult("User profession not found."));
            }
            var professionMap = _mapper.Map<GetUserProfessionDto>(result);
            return new AsyncSuccessDataResult<GetUserProfessionDto>(Task.FromResult(professionMap));
        }

        public async Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByForumPostIdAsync(Guid forumPostId)
        {
            var result = await UserProfessionCurrent.GetAllListAsync(x => x.ForumPostId == forumPostId);
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetUserProfessionDto>>(Task.FromResult("No user professions found."));
            }
            var professionMap = _mapper.Map<List<GetUserProfessionDto>>(result);
            return new AsyncSuccessDataResult<List<GetUserProfessionDto>>(Task.FromResult(professionMap));
        }

        public async Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByUserIdAsync(Guid userId)
        {
            var result = await UserProfessionCurrent.GetAllListAsync(x=>x.UserId == userId);
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetUserProfessionDto>>(Task.FromResult($"No user profession found with the User ID: {userId}."));
            }
            var professionMap = _mapper.Map<List<GetUserProfessionDto>>(result);
            return new AsyncSuccessDataResult<List<GetUserProfessionDto>>(Task.FromResult(professionMap));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> SoftDeleteUserProfessionAsync(Guid id)
        {
            var result = await UserProfessionCurrent.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return new AsyncErrorResult(Task.FromResult($"No user profession found with the ID: {id}"));
            }
            result.IsDeleted = true;
            result.DeletedDate = DateTime.UtcNow;
            await UserProfessionCurrent.DeleteAsync(result);
            return new AsyncSuccessResult(Task.FromResult("User profession deleted successfully."));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> UpdateUserProfessionAsync(UpdateUserProfessionDto professionDto)
        {
            var professionMap = _mapper.Map<UserProfession>(professionDto);
            professionMap.UpdatedDate = DateTime.UtcNow;
            await UserProfessionCurrent.UpdateAsync(professionMap);
            return new AsyncSuccessResult(Task.FromResult("User profession updated successfully."));
        }

        public async Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByUserIdAndForumPostIdAsync(Guid userId, Guid forumPostId)
        {
            var result = await UserProfessionCurrent.GetAllListAsync(x => x.UserId == userId && x.ForumPostId == forumPostId);
            if (result == null)
            {
                return new AsyncErrorDataResult<List<GetUserProfessionDto>>(Task.FromResult("No user profession found."));
            }
            var professionMap = _mapper.Map<List<GetUserProfessionDto>>(result);
            return new AsyncSuccessDataResult<List<GetUserProfessionDto>>(Task.FromResult(professionMap));

        }

        public async Task<IAsyncDataResult<GetUserProfessionDto>> GetActiveUserProfessionByUserIdAsync(Guid userId)
        {
            var result = await UserProfessionCurrent.FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive == true);
            if (result == null)
            {
                return new AsyncErrorDataResult<GetUserProfessionDto>(Task.FromResult($"No user profession found with the User ID: {userId}."));
            }
            var professionMap = _mapper.Map<GetUserProfessionDto>(result);
            return new AsyncSuccessDataResult<GetUserProfessionDto>(Task.FromResult(professionMap));
        }
    }
}
