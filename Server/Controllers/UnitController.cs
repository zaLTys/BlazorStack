using BlazorStack.Server.Data;
using BlazorStack.Shared;
using BlazorStack.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;

        public UnitController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            var existingUnit = await _context.Units.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUnit == null)
                return NotFound("Unit not found");

            existingUnit.Title = unit.Title;
            existingUnit.Attack = unit.Attack;
            existingUnit.Defense = unit.Defense;
            existingUnit.PointCost = unit.PointCost;
            existingUnit.HitPoints = unit.HitPoints;

            await _context.SaveChangesAsync();
            return Ok(existingUnit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var existingUnit = await _context.Units.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUnit == null)
                return NotFound("Unit not found");

            _context.Units.Remove(existingUnit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
    }
}
