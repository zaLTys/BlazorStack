using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorStack.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //emptyExample:
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "zaLTys")
            }, "testAuthenticationType");

            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));           
        }
    }
}
