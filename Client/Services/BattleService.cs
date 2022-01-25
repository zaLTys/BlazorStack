using BlazorStack.Shared;
using System.Net.Http.Json;

namespace BlazorStack.Client.Services
{
    public class BattleService : IBattleService
    {
        private readonly HttpClient _http;

        public BattleService(HttpClient http)
        {
            _http = http;
        }

        public BattleResult LastBattle { get; set; }

        public async Task<BattleResult> StartBattle(int opponentId)
        {
            var result = await _http.PostAsJsonAsync("api/battle", opponentId);
            LastBattle = await result.Content.ReadFromJsonAsync<BattleResult>();
            return LastBattle;
        }
    }
}
