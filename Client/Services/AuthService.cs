using BlazorStack.Shared.Models;
using System.Net.Http.Json;

namespace BlazorStack.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginModel request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>() ?? new ServiceResponse<string> { Success = false, Message = "Error occured" };
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterModel request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>() ?? new ServiceResponse<int> { Success = false, Message="Error occured"};
        }
    }
}
