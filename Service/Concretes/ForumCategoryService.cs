using AutoMapper;
using Core.Utilities.Results;
using Dto.ForumCategories;
using Entity;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class ForumCategoryService : IForumCategoryService
    {
        public ITBaseService<ForumCategory, Guid> ForumCategoryCurrent { get; }
        ITBaseService<ForumCategory, Guid> IDbOperationEvent<ForumCategory,Guid>.Current => ForumCategoryCurrent;
        private readonly IMapper _mapper;

        public ForumCategoryService(IMapper mapper, ITBaseService<ForumCategory,Guid> forumCategoryCurrent)
        {
            _mapper = mapper;
            ForumCategoryCurrent = forumCategoryCurrent;
        }
        public async Task<Core.Utilities.Results.IAsyncResult> CreateCategoryAsync(CreateForumCategoryDto categoryDto)
        {
            var forumCategory = _mapper.Map<ForumCategory>(categoryDto);
            forumCategory.CreatedDate = DateTime.UtcNow;
            var result = await ForumCategoryCurrent.AddAsync(forumCategory);
            return new AsyncSuccessResult(Task.FromResult("Forum category created succesfully"));
        }

        public async Task<IAsyncDataResult<List<GetForumCategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await ForumCategoryCurrent.GetAllListAsync();
            var categoryDtos = _mapper.Map<List<GetForumCategoryDto>>(categories);
            return new AsyncSuccessDataResult<List<GetForumCategoryDto>>(Task.FromResult(categoryDtos));
        }

        public async Task<IAsyncDataResult<GetForumCategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await ForumCategoryCurrent.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category == null)
            {
                return new AsyncErrorDataResult<GetForumCategoryDto>(Task.FromResult("Category not found"));
            }
            var categoryDto = _mapper.Map<GetForumCategoryDto>(category);
            return new AsyncSuccessDataResult<GetForumCategoryDto>(Task.FromResult(categoryDto));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> SoftDeleteCategoryAsync(Guid categoryId)
        {
            var category = await ForumCategoryCurrent.FirstOrDefaultAsync(x=>x.Id == categoryId);
            if (category == null)
            {
                return new AsyncErrorResult(Task.FromResult("Category not found"));
            }
            category.IsDeleted = true;
            category.DeletedDate = DateTime.UtcNow;
            var result = await ForumCategoryCurrent.UpdateAsync(category);
            return new AsyncSuccessResult(Task.FromResult("Category deleted successfully"));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> UpdateCategoryAsync(UpdateForumCategoryDto categoryDto)
        {

            var categoryMapped = _mapper.Map<ForumCategory>(categoryDto);        
            categoryMapped.UpdatedDate = DateTime.UtcNow;
            var result = await ForumCategoryCurrent.UpdateAsync(categoryMapped);  
            return new AsyncSuccessResult(Task.FromResult("Category updated successfully"));
        }
    }
}
