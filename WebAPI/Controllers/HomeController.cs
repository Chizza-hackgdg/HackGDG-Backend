using AutoMapper;
using Dto.Milestones;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;
using System.Diagnostics;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMilestoneService _milestoneService;
        private readonly IUserProfessionService _userProfessionService;
        private readonly IUserService _userService;
        private readonly IGeminiService _geminiService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IGeminiService geminiService, IMapper mapper, IMilestoneService milestoneService, IUserProfessionService userProfessionService, IUserService userService)
        {
            _logger = logger;
            _geminiService = geminiService;
            _mapper = mapper;
            _milestoneService = milestoneService;
            _userProfessionService = userProfessionService;
            _userService = userService;
        }

        [HttpPost("createmilestonejson")]
        public async Task<IActionResult> CreateMilestoneJSON()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUserAsync().Result.Data;
                var response = await _geminiService.CreateJSONMilestones(currentUser.Id);
                if (await response.Success)
                {
                    var jsonResponse = JsonDocument.Parse(await response.Message);
                    var textResponse = jsonResponse
                        .RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text");

                    var textResponseParse = JsonDocument.Parse(textResponse.ToString());
                    var userActiveProfession = await _userProfessionService.GetActiveUserProfessionByUserIdAsync(currentUser.Id);

                    var milestoneDtos = textResponseParse
                        .RootElement
                        .GetProperty("Milestones")
                        .EnumerateArray()
                        .Select(m => new CreateMilestoneDto
                        {
                            UserId = currentUser.Id,
                            ForumPostId = userActiveProfession.Data.Result.Id,
                            MilestoneName = m.GetProperty("MilestoneName").GetString(),
                            GoalDescription = m.GetProperty("GoalDescription").GetString(),
                            CreatedDate = DateTime.UtcNow
                        })
                        .ToList();

                    foreach (var item in milestoneDtos)
                    {
                        item.ForumPostId = userActiveProfession.Data.Result.Id;
                        await _milestoneService.CreateMilestoneAsync(item);
                    }

                    return Ok(milestoneDtos);
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = await response.Message,
                        Errors = await response.Messages
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, new { Message = "An internal server error occurred." });
            }
        }

        [HttpPost("chatbotresponse")]
        public async Task<IActionResult> ChatBotResponse([FromBody] string request)
        {
            try
            {
                var response = await _geminiService.ChatBotResponse(request);
                if (await response.Success)
                {
                    var jsonResponse = JsonDocument.Parse(await response.Message);
                    var textMessage = jsonResponse
                        .RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return Ok(textMessage);
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = await response.Message,
                        Errors = await response.Messages
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, new { Message = "An internal server error occurred." });
            }
        }
    }
}
