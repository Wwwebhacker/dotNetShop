using Items.Messages;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly HttpContextService contextService;
        private readonly OrderRepo orderRepo;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(HttpContextService contextService, OrderRepo orderRepo, IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
            this.contextService = contextService;
            this.orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            return Ok(orderRepo.GetOrders(contextService.getUser()));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(List<int> productIds)
        {

            //return Ok(new OrderCreatedMessage
            //{
            //    user = contextService.getUser(),
            //    productIds = productIds
            //});
            //orderRepo.AddOrder(contextService.getUser(), productIds);
            await _publishEndpoint.Publish(new OrderCreatedMessage
            {
                user = contextService.getUser(),
                productIds = productIds
            });

            return Ok();
        }
    }
}
