using BlazorStack.Shared.Entities;

namespace BlazorStack.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}
