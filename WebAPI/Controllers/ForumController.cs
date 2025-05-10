using Dto.ForumCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/forum")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumCategoryService _forumCategoryService;

        public ForumController(IForumCategoryService forumCategoryService)
        {
            _forumCategoryService = forumCategoryService;
        }

        [HttpGet("getallforumcategories")]
        public async Task<IActionResult> GetAllForumCategories()
        {
            var result = _forumCategoryService.GetAllCategoriesAsync().Result;
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

        [HttpGet("getforumcategorybyid/{categoryId}")]
        public async Task<IActionResult> GetForumCategoryById(Guid categoryId)
        {
            var result = _forumCategoryService.GetCategoryByIdAsync(categoryId).Result;
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("createforumcategory")]
        public async Task<IActionResult> CreateForumCategory([FromBody] CreateForumCategoryDto categoryDto)
        {
            var result = await _forumCategoryService.CreateCategoryAsync(categoryDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("updateforumcategory")]
        public async Task<IActionResult> UpdateForumCategory([FromBody] UpdateForumCategoryDto categoryDto)
        {
            var result = await _forumCategoryService.UpdateCategoryAsync(categoryDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("deleteforumcategory/{categoryId}")]
        public async Task<IActionResult> DeleteForumCategory(Guid categoryId)
        {
            var result = await _forumCategoryService.SoftDeleteCategoryAsync(categoryId);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
