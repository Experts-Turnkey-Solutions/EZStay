using EZStay.UI.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EZStay.UI.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("EZStayApi");
        }

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("User");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
                }
                else
                {
                    // For debugging purposes, print the status code
                    Console.WriteLine($"Error fetching users: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception fetching users: {ex.Message}");
            }

            return new List<UserViewModel>();
        }
    }
}