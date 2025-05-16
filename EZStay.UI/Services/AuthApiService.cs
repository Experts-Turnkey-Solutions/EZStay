using System.Net.Http.Json;
using EZStay.UI.Models;

namespace EZStay.UI.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("EZStayApi");
        }

        public async Task<LoginResponse> LoginAsync(string emailOrUsername, string password, string role)
        {
            var loginDto = new
            {
                EmailOrUsername = emailOrUsername,
                Password = password,
                Role = role
            };

            var response = await _httpClient.PostAsJsonAsync("Auth/login", loginDto);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString); // or debug/log it

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }

            return null;
        }
    }

    public class LoginResponse
    {
        public UserViewModel User { get; set; }
        public string Token { get; set; }
    }
}