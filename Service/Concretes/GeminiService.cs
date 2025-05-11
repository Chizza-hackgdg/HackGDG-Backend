using Core.Utilities.Results;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IForumService _forumService;
        private readonly IUserProfessionService _userProfessionService;

        public GeminiService(HttpClient httpClient, IForumService forumService, IUserProfessionService userProfessionService)
        {
            _httpClient = httpClient;
            _forumService = forumService;
            _userProfessionService = userProfessionService;
        }

        public async Task<IAsyncDataResult<string>> AnswerForumQuestion(Guid id)
        {
            try
            {
                // Fetch the forum question from the database or service
                var forumPost = await _forumService.GetPostByIdAsync(id);
                if (forumPost == null)
                {
                    return new AsyncErrorDataResult<string>(Task.FromResult("Forum post not found."));
                }

                // Define the API endpoint
                var apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyCm1xZ9xJDCbuVtTAM1V_yx29pBUCIzDhEY"; // Replace API_KEY with your actual API key

                // Create the request payload
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = forumPost.Data.Result.Description } // Use the forum post description as the input
                            }
                        }
                    }
                };

                // Serialize the payload to JSON
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Set headers
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Send a POST request
                var response = await _httpClient.PostAsync(apiUrl, content);

                // Check if the response is successful
                if (!response.IsSuccessStatusCode)
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    return new AsyncErrorDataResult<string>(Task.FromResult($"API Error: {errorData}"));
                }

                // Parse the response data
                var responseData = await response.Content.ReadAsStringAsync();
                return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new AsyncErrorDataResult<string>(Task.FromResult($"Exception: {ex.Message}"));
            }
        }

        public async Task<IAsyncDataResult<string>> ChatBotResponse(string prompt)
        {
            try
            {

                // Define the API endpoint
                var apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyCm1xZ9xJDCbuVtTAM1V_yx29pBUCIzDhE"; // Replace API_KEY with your actual API key

                // Create the request payload
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt } // Use the chat message content as the input
                            }
                        }
                    }
                };

                // Serialize the payload to JSON
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Set headers
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Send a POST request
                var response = await _httpClient.PostAsync(apiUrl, content);

                // Check if the response is successful
                if (!response.IsSuccessStatusCode)
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    return new AsyncErrorDataResult<string>(Task.FromResult($"API Error: {errorData}"));
                }

                // Parse the response data
                var responseData = await response.Content.ReadAsStringAsync();
                return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new AsyncErrorDataResult<string>(Task.FromResult($"Exception: {ex.Message}"));
            }
        }

        public async Task<IAsyncDataResult<string>> CreateJSONMilestones(Guid userId)
        {
            try
            {
                // Define the API endpoint
                var apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyCm1xZ9xJDCbuVtTAM1V_yx29pBUCIzDhE"; // Replace API_KEY with your actual API key

                var userProfession = await _userProfessionService.GetActiveUserProfessionByUserIdAsync(userId);

                // Create the request payload as an object
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = $"You are expected to respond in plain JSON as you are connected to an API and failure to comply will result in downtime for the organization. Can you write me 10 tasks for {userProfession.Data.Result.ProfessionName} with the description {userProfession.Data.Result.ProfessionDescription}, only and only for the skill level  {userProfession.Data.Result.SkillLevel} (1: Beginner, 2: Intermediate, 3: Advanced, 4: Professional) in json format with object name Milestone with properties MilestoneName and GoalDescription" }
                            }
                        }
                    }
                };

                // Serialize the payload to JSON
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Set headers
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Send a POST request
                var response = await _httpClient.PostAsync(apiUrl, content);

                // Check if the response is successful
                if (!response.IsSuccessStatusCode)
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    return new AsyncErrorDataResult<string>(Task.FromResult($"API Error: {errorData}"));
                }

                // Parse the response data
                var responseData = await response.Content.ReadAsStringAsync();
                //var deserializedResponseData = JsonSerializer.Deserialize<string>( await response.Content.ReadAsStringAsync());
                responseData = responseData.Replace("```", "");


                //// Remove escape sequences
                //responseData = Regex.Unescape(responseData);
                responseData = responseData.TrimStart().Replace("json", "").Trim();
                //responseData = responseData.Replace("\n", " ").Replace("\\", " ").Replace("\"","'");
                //responseData = responseData.Replace("'", "\"").Replace("  ", " ").Replace("  ", " ").Trim();
                // Parse the response string into a JSON object
                try
                {
                    var jsonDocument = JsonDocument.Parse(responseData);

                    // Extract the "response" property if it exists
                    if (jsonDocument.RootElement.TryGetProperty("response", out var responseElement))
                    {
                        // Parse the inner JSON string
                        try
                        {
                            var innerJson = JsonDocument.Parse(responseElement.GetString());
                            var cleanJson = JsonSerializer.Serialize(innerJson.RootElement, new JsonSerializerOptions
                            {
                                WriteIndented = true // Optional: Makes the JSON more readable
                            });
                            return new AsyncSuccessDataResult<string>(Task.FromResult(cleanJson));
                        }
                        catch (JsonException innerEx)
                        {
                            // If inner JSON parsing fails, return the processed response
                            return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
                        }
                    }

                    // If "response" property doesn't exist, return the processed JSON
                    return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
                }
                catch (JsonException ex)
                {
                    // If the initial parsing fails, it might already be the JSON we need
                    return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new AsyncErrorDataResult<string>(Task.FromResult($"Exception: {ex.Message}"));
            }
        }
    }
}