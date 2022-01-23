using BlazorStack.Shared.Models;

namespace BlazorStack.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterModel request);
        Task<ServiceResponse<string>> Login(UserLoginModel request);
    };
}
