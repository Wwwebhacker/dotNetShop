using Items.Consumers;
using Items.Models;

namespace Items
{
    public class NotificationService
    {
        public ILogger<OrderConsumer> Logger { get; }

        public NotificationService(ILogger<OrderConsumer> logger)
        {
            Logger = logger;
        }
        public Task SendSuccessPurchaseEmailAsync(string userEmail, List<Product> products)
        {
            Logger.LogCritical($"Email to {userEmail}: Successfully purchased {string.Join(", ", products.Select(p => p.Name))}.");

            return Task.CompletedTask;
        }

        public Task SendFailedPurchaseEmailAsync(string userEmail, Product product)
        {
            Logger.LogCritical($"Email to {userEmail}: Failed to purchase {product.Name}. Insufficient inventory.");
            return Task.CompletedTask;
        }
    }
}
