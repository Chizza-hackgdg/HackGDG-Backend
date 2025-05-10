using Core.Utilities.Results;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;

        public GeminiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IAsyncDataResult<string>> GetResponseFromGeminiAsync(string prompt)
        {
            try
            {

                var apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyCm1xZ9xJDCbuVtTAM1V_yx29pBUCIzDhE"; // Replace API_KEY with your 


                var payload = new
                {
                    
                    contents = new[]
                    {
                            new
                            {
                                parts = new[]
                                {
                                    new { text = prompt }
                                }
                            }
                        }
                };


                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

              
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            
                var response = await _httpClient.PostAsync(apiUrl, content);
                Console.WriteLine($"API Response status: {response.StatusCode}");
                Console.WriteLine($"API Response content: {await response.Content.ReadAsStringAsync()}");
               
                Console.WriteLine($"API Response status: {response.StatusCode}");

                
                if (!response.IsSuccessStatusCode)
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {errorData}");
                    return new AsyncErrorDataResult<string>(
                        Task.FromResult($"API Error: {errorData}")
                    );
                }

                
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response data: {responseData}");

                return new AsyncSuccessDataResult<string>(Task.FromResult(responseData));
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Server Error: {ex.Message}");
                return new AsyncErrorDataResult<string>(
                    Task.FromResult($"Exception: {ex.Message}")
                );
            }
        }
    }
}
