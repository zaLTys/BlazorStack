using Blazored.Toast.Services;
using BlazorStack.Shared;

namespace BlazorStack.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;

        public UnitService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public IList<Unit> Units => new List<Unit>()
        {
            new Unit { Id = 1, Title = "Tank", Attack = 5, Defense = 20, PointCost = 100 },
            new Unit { Id = 2, Title = "Assasin", Attack = 20, Defense = 5, PointCost = 100 },
            new Unit { Id = 3, Title = "Ranged", Attack = 15, Defense = 5, PointCost = 80 }
        };
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints});
            _toastService.ShowSuccess($"{unit.Title} built successfully", "Success!");
        }
    }
}
