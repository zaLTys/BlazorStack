using BlazorStack.Server.Data;
using BlazorStack.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorStack.Server.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilityService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> GetUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.Id == userId);
            return user;
        }
    }
}
