using BlazorStack.Server.Data;
using BlazorStack.Server.Services;
using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(x => x.Id == unitId);
            var user = await _utilityService.GetUser();
            if (user.Points < unit.PointCost)
            {
                return BadRequest("NotEnoughPoints"); //ToDo: return serviceResponse
            }

            user.Points -= unit.PointCost;
            var newUserUnit = new UserUnit
            {
                UnitId = unitId,
                UserId = user.Id,
                HitPoints = unit.HitPoints,
            };

            _context.UserUnits.Add(newUserUnit);
            await _context.SaveChangesAsync();

            return Ok(newUserUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserUnits()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits.Where(x => x.UserId == user.Id).ToListAsync();

            var response = userUnits.Select(x => new UserUnitResponse
            {
                UnitId = x.UnitId,
                HitPoints = x.HitPoints,
            });
            return Ok(response);
        }

        [HttpPost("resurrect")]
        public async Task<IActionResult> ResurrectArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(x=> x.UserId == user.Id)
                .Include(u => u.Unit).ToListAsync();

            int pointCost = 1000;

            if(user.Points < pointCost)
            {
                return BadRequest("Not enough points to revive army");
            }

            var armyAlreadyAlive = true;
            foreach (var userUnit in userUnits)
            {
                if(userUnit.HitPoints <= 0)
                {
                    armyAlreadyAlive = false;
                    userUnit.HitPoints = new Random().Next(0,userUnit.Unit.HitPoints);
                }

            }

            if (armyAlreadyAlive)
                return Ok("Army is already alive");

            user.Points -= pointCost;

            await _context.SaveChangesAsync();

            return Ok("Army revived");
        }
    }
}
