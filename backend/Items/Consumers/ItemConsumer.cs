using Items.Messages;
using Items.Repos;
using MassTransit;

namespace Items.Consumers
{
    public class ItemConsumer :
        IConsumer<ItemCreatedMessage>,
        IConsumer<ItemUpdateMessage>,
        IConsumer<ItemDeletedMessage>
    {
        private readonly IItemRepo _itemRepository;
        public ItemConsumer(IItemRepo itemRepo)
        {
            _itemRepository = itemRepo;
        }
        public Task Consume(ConsumeContext<ItemCreatedMessage> context)
        {
            _itemRepository.AddItem(new Models.Item
            {
                Name = context.Message.Name,
                Description = context.Message.Description,
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ItemUpdateMessage> context)
        {
            _itemRepository.UpdateItem(new Models.Item
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Description = context.Message.Description,
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ItemDeletedMessage> context)
        {
            _itemRepository.DeleteItem(context.Message.Id);

            return Task.CompletedTask;
        }
    }
}
