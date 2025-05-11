using Dto.ForumCategories;
using Dto.ForumComments;
using Dto.ForumPosts;
using Entity;
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
        private readonly IForumService _forumService;

        public ForumController(IForumCategoryService forumCategoryService, IForumService forumService)
        {
            _forumCategoryService = forumCategoryService;
            _forumService = forumService;
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

        [HttpPost("createpost")]
        public async Task<IActionResult> CreateForumPost(CreateForumPostDto request)
        {
            var result = await _forumService.CreatePostAsync(request);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getallposts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _forumService.GetAllPostsAsync();
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getpostbyid/{postId}")]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var result = await _forumService.GetPostByIdAsync(postId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("updatepost")]
        public async Task<IActionResult> UpdateForumPost(UpdateForumPostDto request)
        {
            var result = await _forumService.UpdatePostAsync(request);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("deletepost/{postId}")]
        public async Task<IActionResult> DeleteForumPost(Guid postId)
        {
            var result = await _forumService.SoftDeletePostAsync(postId);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("createcomment")]
        public async Task<IActionResult> CreateForumComment(CreateForumCommentDto request)
        {
            var result = await _forumService.CreateCommentAsync(request);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getallcomments")]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _forumService.GetAllCommentsAsync();
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getcommentbyid/{commentId}")]
        public async Task<IActionResult> GetCommentById(Guid commentId)
        {
            var result = await _forumService.GetCommentByIdAsync(commentId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getcommentsbypostid/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            var result = await _forumService.GetCommentsByPostIdAsync(postId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getchildcommentsbymainid/{commentId}")]
        public async Task<IActionResult> GetChildCommentsByMainId(Guid commentId)
        {
            var result = await _forumService.GetChildCommentsByMainId(commentId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("updatecomment")]
        public async Task<IActionResult> UpdateForumComment(UpdateForumCommentDto request)
        {
            var result = await _forumService.UpdateCommentAsync(request);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("deletecomment/{commentId}")]
        public async Task<IActionResult> DeleteForumComment(Guid commentId)
        {
            var result = await _forumService.SoftDeleteCommentAsync(commentId);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("submitforummatch")]
        public async Task<IActionResult> SubmitForForumPostMatch(SubmitForumMatchDto request)
        {
            var result = await _forumService.SubmitForForumPostMatchAsync(request);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("matchallusersforforum")]

        public async Task<IActionResult> MatchAllUsersForAllForumPosts()
        {
            var result = await _forumService.MatchAllUsersForAllForumPostsAsync();
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }



    }
}

