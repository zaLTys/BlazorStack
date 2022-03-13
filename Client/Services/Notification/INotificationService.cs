namespace BlazorStack.Client.Services.Notification
{
    public interface INotificationService
    {
        int Count { get; set; }

        void IncrementCounter();
        void ResetCount();

        event Action OnChange;
    }
}
