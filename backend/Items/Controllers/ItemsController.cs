using Items.Messages;
using Items.Models;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {

        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IItemRepo _itemRepository;

        public ItemsController(IItemRepo itemRepository, IPublishEndpoint publishEndpoint)
        {
            _itemRepository = itemRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Ok(_itemRepository.GetItems());
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = _itemRepository.GetItem(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Item item)
        {
            //_itemRepository.AddItem(item);
            //return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);

            await _publishEndpoint.Publish(new ItemCreatedMessage
            {
                Name = item.Name,
                Description = item.Description,
            });


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item updatedItem)
        {
            if (id != updatedItem.Id) return BadRequest();

            //_itemRepository.UpdateItem(updatedItem);
            //return NoContent();

            await _publishEndpoint.Publish(new ItemUpdateMessage()
            {
                Id = id,
                Name = updatedItem.Name,
                Description = updatedItem.Description,
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            //_itemRepository.DeleteItem(id);
            //return NoContent();

            await _publishEndpoint.Publish(new ItemDeletedMessage()
            {
                Id = id
            });

            return Ok();
        }
    }
}
