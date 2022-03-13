using Microsoft.AspNetCore.Components;

namespace BlazorStack.Client.Pages
{
    public partial class Counter : ComponentBase
    {
        private int currentCount = 0;
        private string notificationContent = string.Empty;

        private void IncrementCount()
        {
            currentCount++; //default
            NotificationService.IncrementCounter(); //for service
        }


        private void ResetCount()
        {
            currentCount = 0;
            notificationContent = "Counter has been reset.";
        }
    }
}
