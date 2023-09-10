using Items.Messages;
using Items.Models;
using Items.Repos;
using MassTransit;

namespace Items.Consumers
{
    public class OrderConsumer :
        IConsumer<OrderCreatedMessage>
    {
        private readonly OrderRepo orderRepo;
        private readonly EfProductRepo _productRepository;
        private readonly NotificationService notificationService;
        private readonly ILogger<OrderConsumer> _logger;


        public OrderConsumer(OrderRepo orderRepo, EfProductRepo productRepository, NotificationService notificationService, ILogger<OrderConsumer> logger)
        {
            _productRepository = productRepository;
            this.notificationService = notificationService;
            this.orderRepo = orderRepo;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            using (var transaction = _productRepository.BeginTransaction())
            {
                try
                {
                    var message = context.Message;
                    string userEmail = message.user.Email;
                    var products = new List<Product>();

                    foreach (var id in context.Message.productIds)
                    {
                        var product = _productRepository.GetProduct(id);
                        if (product == null)
                        {
                            transaction.Rollback();
                            await notificationService.SendFailedPurchaseEmailAsync(userEmail, product);
                            return;
                        }

                        product.InventoryCount -= 1;
                        if (product.InventoryCount < 0)
                        {
                            transaction.Rollback();
                            await notificationService.SendFailedPurchaseEmailAsync(userEmail, product);
                            return;
                        }
                        _productRepository.UpdateProduct(product);
                        products.Add(product);
                    }

                    orderRepo.AddOrder(context.Message.user, context.Message.productIds);
                    transaction.Commit();
                    await notificationService.SendSuccessPurchaseEmailAsync(userEmail, products);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while processing the order: {ex.Message}");
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
