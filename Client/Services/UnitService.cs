using Blazored.Toast.Services;
using BlazorStack.Shared.Entities;
using System.Net.Http.Json;

namespace BlazorStack.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;
        private readonly IPointService _pointService;

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public UnitService(IToastService toastService, HttpClient http, IPointService pointService)
        {
            _toastService = toastService;
            _http = http;
            _pointService = pointService;
        }


        public async Task AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            var result = await _http.PostAsJsonAsync<int>("api/userunit", unitId);
            if(result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                await _pointService.GetPoints();
                _toastService.ShowSuccess($"{unit.Title} built successfully", "Success!");
            }          
        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("api/unit");
            }
        }

        public async Task LoadUserUnitsAsync()
        {
            var units = await _http.GetFromJsonAsync<IList<UserUnit>>("api/userunit");

            MyUnits = units;
        }

        public async Task ReviveArmy()
        {
            var result = await _http.PostAsJsonAsync<string>("api/userUnit/resurrect", null);
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowSuccess(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }

            await LoadUserUnitsAsync();
            await _pointService.GetPoints();
        }
    }
}
