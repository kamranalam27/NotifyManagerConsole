using NotificationServiceLibrary.Models;
using NotificationServiceLibrary.Services;
using NotificationServiceLibrary.Providers;
using NotificationServiceLibrary.Interfaces;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Notification Service Test Application");

        // Step 1: Configure providers
        var providers = new Dictionary<NotificationType, INotificationProvider>
        {
            { NotificationType.Email, new EmailNotificationProvider() },
            { NotificationType.Push, new PushNotificationProvider() }
        };

        // Step 2: Initialize the NotificationService
        var notificationService = new NotificationService(providers);

        // Step 3: Create test notifications
        var requests = new List<NotificationRequest>
        {
            new NotificationRequest
            {
                Type = NotificationType.Email,
                Recipient = "user1@example.com",
                Message = "Breaking news: Console testing in progress!"
            },
            new NotificationRequest
            {
                Type = NotificationType.Push,
                Recipient = "device_token_123",
                Message = "New alert: Check your notifications."
            },
            new NotificationRequest
            {
                Type = NotificationType.SMS, // Unsupported type for testing error handling
                Recipient = "+1234567890",
                Message = "This is an SMS message."
            }
        };

        // Step 4: Send notifications
        var results = await notificationService.SendNotificationsAsync(requests);

        // Step 5: Display results
        foreach (var result in results)
        {
            Console.WriteLine("Notification Result:");
            Console.WriteLine($"  ID: {result.NotificationId}");
            Console.WriteLine($"  Recipient: {result.Recipient}");
            Console.WriteLine($"  Success: {result.Success}");
            Console.WriteLine($"  Provider: {result.Provider}");
            Console.WriteLine($"  Error: {result.ErrorMessage}");
            Console.WriteLine(new string('-', 40));
        }

        Console.WriteLine("Notification testing completed.");
    }
}
