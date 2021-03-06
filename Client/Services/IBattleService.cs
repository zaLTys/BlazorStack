using BlazorStack.Shared;

namespace BlazorStack.Client.Services
{
    public interface IBattleService
    {
        Task<BattleResult> StartBattle(int opponentId);
        BattleResult LastBattle { get; set; }

        IList<BattleHistoryEntry> History { get; set; }

        Task GetHistory();
    }
}
