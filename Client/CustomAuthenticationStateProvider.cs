using Blazored.LocalStorage;
using BlazorStack.Client.Helpers;
using BlazorStack.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlazorStack.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly IPointService _pointService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService,
            HttpClient http, IPointService pointService)
        {
            _localStorageService = localStorageService;
            _http = http;
            _pointService = pointService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"",""));
                    await _pointService.GetPoints();
                }
                catch (Exception)
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            };

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
