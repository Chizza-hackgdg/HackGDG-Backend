using Core.Utilities.Results;
using Dto.UserProfessions;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Service.Abstracts
{
    public interface IUserProfessionService:IDbOperationEvent<UserProfession,Guid>
    {
        Task<IAsyncResult> CreateUserProfessionAsync(CreateUserProfessionDto professionDto);
        Task<IAsyncResult> UpdateUserProfessionAsync(UpdateUserProfessionDto professionDto);
        Task<IAsyncResult> SoftDeleteUserProfessionAsync(Guid id);
        Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetAllUserProfessionsAsync();
        Task<IAsyncDataResult<GetUserProfessionDto>> GetUserProfessionByIdAsync(Guid id);
        Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByUserIdAsync(Guid userId);
        Task<IAsyncDataResult<GetUserProfessionDto>> GetActiveUserProfessionByUserIdAsync(Guid userId);
        Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByForumPostIdAsync(Guid forumPostId);
        Task<IAsyncDataResult<List<GetUserProfessionDto>>> GetUserProfessionsByUserIdAndForumPostIdAsync(Guid userId, Guid forumPostId);
    }
}
