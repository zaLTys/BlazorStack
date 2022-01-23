using System.Net.Http.Json;

namespace BlazorStack.Client.Services
{
    public class PointService : IPointService
    {
        private readonly HttpClient _http;

        public PointService(HttpClient http)
        {
            _http = http;
        }
        public int Points { get; set; } = 1000;

        public event Action OnChange;

        public async Task AddPoints(int amount)
        {
            var result = await _http.PutAsJsonAsync<int>("api/user/addpoints", amount);
            Points = await result.Content.ReadFromJsonAsync<int>();
            PointsChanged();
        }

        public async Task GetPoints()
        {
            Points = await _http.GetFromJsonAsync<int>("api/user/getpoints");
            PointsChanged();
        }

        public void SpendPoints(int amount)
        {
            Points -= amount;
            PointsChanged();
        }

        void PointsChanged() => OnChange.Invoke();
    }
}
