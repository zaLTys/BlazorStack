using BlazorStack.Shared.Entities;

namespace BlazorStack.Client.Services
{
    public interface ILeaderboardService
    {
        IList<UserStatistic> Leaderboard { get; set; }
        Task GetLeaderboard();
    }
}
