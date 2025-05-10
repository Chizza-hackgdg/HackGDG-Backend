using Core.Utilities.Results;
using Dto.ForumCategories;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Service.Abstracts
{
    public interface IForumCategoryService: IDbOperationEvent<ForumCategory,Guid>
    {
        Task<IAsyncDataResult<List<GetForumCategoryDto>>> GetAllCategoriesAsync();
        Task<IAsyncDataResult<GetForumCategoryDto>> GetCategoryByIdAsync(Guid categoryId);
        Task<IAsyncResult> CreateCategoryAsync(CreateForumCategoryDto categoryDto);
        Task<IAsyncResult> UpdateCategoryAsync(UpdateForumCategoryDto categoryDto);
        Task<IAsyncResult> SoftDeleteCategoryAsync(Guid categoryId);
    }
}
