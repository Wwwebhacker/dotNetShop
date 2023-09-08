using Items.Messages;
using Items.Models;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IPublishEndpoint _publishEndpoint;
        private readonly EfProductRepo _productRepository;

        public ProductsController(EfProductRepo productRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_productRepository.GetProducts());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            //return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);

            await _publishEndpoint.Publish(new ProductCreatedMessage
            {
                Name = product.Name,
                Description = product.Description,
            });


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id) return BadRequest();

            //_itemRepository.UpdateItem(updatedItem);
            //return NoContent();

            await _publishEndpoint.Publish(new ProductUpdateMessage()
            {
                Id = id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //_itemRepository.DeleteItem(id);
            //return NoContent();

            await _publishEndpoint.Publish(new ProductDeletedMessage()
            {
                Id = id
            });

            return Ok();
        }
    }
}
