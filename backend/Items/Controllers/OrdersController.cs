using Items.Messages;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly OrderRepo orderRepo;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(OrderRepo orderRepo, IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
            this.orderRepo = orderRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(List<int> itemIds)
        {
            await _publishEndpoint.Publish(new OrderCreatedMessage
            {
                itemIds = itemIds
            });

            return Ok();
        }
    }
}
