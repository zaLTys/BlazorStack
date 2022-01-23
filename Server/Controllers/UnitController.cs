using BlazorStack.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorStack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        public IList<Unit> Units => new List<Unit>()
        {
            new Unit { Id = 1, Title = "Tank", Attack = 5, Defense = 20, PointCost = 100 },
            new Unit { Id = 2, Title = "Assasin", Attack = 20, Defense = 5, PointCost = 100 },
            new Unit { Id = 3, Title = "Ranged", Attack = 15, Defense = 5, PointCost = 80 }
        };

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            return Ok(Units);
        }
    }
}
