using BlazorStack.Server.Data;
using BlazorStack.Server.Services;
using BlazorStack.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpGet("getpoints")]
        public async Task<IActionResult> GetPoints()
        {
            var user = await _utilityService.GetUser();
            return Ok(user?.Points ?? 0);
        }
        [HttpPut("addpoints")]
        public async Task<IActionResult> AddPoints([FromBody]int points)
        {
            var user = await _utilityService.GetUser();
            user.Points += points;

            await _context.SaveChangesAsync();
            return Ok(user.Points);
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var users = await _context.Users.Where(x => !x.IsDeleted && x.AcceptedTermsAgreements).ToListAsync();

            users = users.OrderByDescending(x=> x.Victories)
                .ThenBy(x=> x.Defeats)
                .ThenBy(x=> x.DateOfBirth)
                .ToList();

            int rank = 1;
            var response = users.Select(x => new UserStatistic
            {
                Rank = rank++,
                UserId = x.Id,
                Username = x.Username,
                Battles = x.Battles,
                Victories = x.Victories,
                Defeats = x.Defeats,
            });

            return Ok(response);
        }
    }
}
