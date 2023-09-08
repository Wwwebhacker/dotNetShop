using Items.Messages;
using Items.Repos;
using MassTransit;

namespace Items.Consumers
{
    public class OrderConsumer :
        IConsumer<OrderCreatedMessage>
    {
        private readonly OrderRepo orderRepo;
        public OrderConsumer(OrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }
        public Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            orderRepo.AddOrder(context.Message.itemIds);

            return Task.CompletedTask;
        }
    }
}
