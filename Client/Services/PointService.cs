namespace BlazorStack.Client.Services
{
    public class PointService : IPointService
    {
        public int Points { get; set; } = 1000;

        public event Action OnChange;

        public void AddPoints(int amount)
        {
            Points += amount;
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
