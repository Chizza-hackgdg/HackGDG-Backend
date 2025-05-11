using Dto.UserProfessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace WebAPI.Controllers
{
    [Route("api/profession")]
    [ApiController]
    
    
    public class UserProfessionController : ControllerBase
    {
        private readonly IUserProfessionService _userProfessionService;
        public UserProfessionController(IUserProfessionService userProfessionService)
        {
            _userProfessionService = userProfessionService;
        }
        [HttpGet("getalluserprofessions")]
        public async Task<IActionResult> GetAllUserProfessions()
        {
            var result = await _userProfessionService.GetAllUserProfessionsAsync();
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getuserprofessionbyid/{id}")]
        public async Task<IActionResult> GetUserProfessionById(Guid id)
        {
            var result = await _userProfessionService.GetUserProfessionByIdAsync(id);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getuserprofessionsbyforumpostid/{forumPostId}")]
        public async Task<IActionResult> GetUserProfessionsByForumPostId(Guid forumPostId)
        {
            var result = await _userProfessionService.GetUserProfessionsByForumPostIdAsync(forumPostId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("createuserprofession")]
        public async Task<IActionResult> CreateUserProfession([FromBody] CreateUserProfessionDto professionDto)
        {
            var result = await _userProfessionService.CreateUserProfessionAsync(professionDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("updateuserprofession")]
        public async Task<IActionResult> UpdateUserProfession([FromBody] UpdateUserProfessionDto professionDto)
        {
            var result = await _userProfessionService.UpdateUserProfessionAsync(professionDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("deleteuserprofession/{id}")]
        public async Task<IActionResult> DeleteUserProfession(Guid id)
        {
            var result = await _userProfessionService.SoftDeleteUserProfessionAsync(id);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getuserprofessionbyuserid/{userId}")]
        public async Task<IActionResult> GetUserProfessionsByUserId(Guid userId)
        {
            var result = await _userProfessionService.GetUserProfessionsByUserIdAsync(userId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        
      

        

        


    }
}
