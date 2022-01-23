namespace BlazorStack.Client.Services
{
    public interface IPointService
    {
        event Action OnChange;
        int Points { get; set; }
        void SpendPoints(int amount);
        void AddPoints(int amount);
        Task GetPoints();
    }
}
