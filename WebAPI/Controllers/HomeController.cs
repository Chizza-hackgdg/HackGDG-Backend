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
        private readonly IGeminiService _geminiService;

        public HomeController(ILogger<HomeController> logger, IGeminiService geminiService)
        {
            _logger = logger;
            _geminiService = geminiService;
        }

        [HttpPost("sendpromptgemini")]
        public async Task<IActionResult> SendPromptToGemini(string text)
        {
            try
            {
                var response = await _geminiService.GetResponseFromGeminiAsync(text);

                if (await response.Success)
                {
                    var jsonResponse = JsonDocument.Parse(await response.Message);
                    var textResponse = jsonResponse
                        .RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return Ok(new { Response = textResponse });
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
