using Blazored.Toast.Services;
using BlazorStack.Shared;
using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;
using System.Net.Http.Json;

namespace BlazorStack.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnitModel> MyUnits { get; set; } = new List<UserUnitModel>();

        public UnitService(IToastService toastService, HttpClient http)
        {
            _toastService = toastService;
            _http = http;
        }


        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnitModel { UnitId = unit.Id, HitPoints = unit.HitPoints });
            _toastService.ShowSuccess($"{unit.Title} built successfully", "Success!");
        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("api/Unit") ?? new List<Unit>();
            }
        }
    }
}
