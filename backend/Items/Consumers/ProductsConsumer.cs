using Items.Messages;
using Items.Repos;
using MassTransit;

namespace Items.Consumers
{
    public class ProductsConsumer :
        IConsumer<ProductCreatedMessage>,
        IConsumer<ProductUpdateMessage>,
        IConsumer<ProductDeletedMessage>
    {
        private readonly EfProductRepo _productRepository;
        public ProductsConsumer(EfProductRepo productRepository)
        {
            _productRepository = productRepository;
        }
        public Task Consume(ConsumeContext<ProductCreatedMessage> context)
        {
            _productRepository.AddProduct(new Models.Product
            {
                Name = context.Message.Name,
                Description = context.Message.Description,
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ProductUpdateMessage> context)
        {
            _productRepository.UpdateProduct(new Models.Product
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                Description = context.Message.Description,
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ProductDeletedMessage> context)
        {
            _productRepository.DeleteProduct(context.Message.Id);

            return Task.CompletedTask;
        }
    }
}
