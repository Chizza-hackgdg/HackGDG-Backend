using Dto.Milestones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;

namespace WebAPI.Controllers
{
    [Route("api/milestones")]
    [ApiController]

    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneService _mileStoneService;

        public MilestoneController(IMilestoneService mileStoneService)
        {
            _mileStoneService = mileStoneService;
        }

        [HttpGet("getallmilestones")]
        public async Task<IActionResult> GetAllMilestones()
        {
            var result = await _mileStoneService.GetAllMilestonesAsync();
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getmilestonebyid/{milestoneId}")]
        public async Task<IActionResult> GetMilestoneById(Guid milestoneId)
        {
            var result = await _mileStoneService.GetMilestoneByIdAsync(milestoneId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("createmilestone")]
        public async Task<IActionResult> CreateMilestone([FromBody] CreateMilestoneDto milestoneDto)
        {
            var result = await _mileStoneService.CreateMilestoneAsync(milestoneDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("updatemilestone")]
        public async Task<IActionResult> UpdateMilestone([FromBody] UpdateMilestoneDto milestoneDto)
        {
            var result = await _mileStoneService.UpdateMilestoneAsync(milestoneDto);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("deletemilestone/{milestoneId}")]
        public async Task<IActionResult> DeleteMilestone(Guid milestoneId)
        {
            var result = await _mileStoneService.SoftDeleteMilestoneAsync(milestoneId);
            if (await result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getmilestonesbyforumpostid/{forumPostId}")]
        public async Task<IActionResult> GetMilestonesByForumPostId(Guid forumPostId)
        {
            var result = await _mileStoneService.GetMilestonesByForumPostIdAsync(forumPostId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }
        [HttpGet("getmilestonesbyuserid/{userId}")]
        public async Task<IActionResult> GetMilestonesByUserId(Guid userId)
        {
            var result = await _mileStoneService.GetMilestonesByUserIdAsync(userId);
            if (await result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


    }
}
