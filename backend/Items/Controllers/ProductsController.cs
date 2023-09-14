using Items.Dto;
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

        [HttpGet("Files/{filename}")]
        public IActionResult GetImage(string filename)
        {
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "images");
            var imagePath = Path.Combine(imageDirectory, filename);
            if (Path.Exists(imagePath))
            {
                var imageFileStream = System.IO.File.OpenRead(imagePath);
                return File(imageFileStream, "image/jpeg"); // Adjust MIME type based on your image.
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateDto productDto)
        {
            if (productDto.Image == null)
            {
                return BadRequest("Image upload failed.");
            }

            var newFilePath = await updloadImage(productDto.Image);

            //if (productDto.Image == null || productDto.Image.Length <= 0)
            //{
            //    return BadRequest("Image upload failed.");
            //}

            //var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images");

            //// Check if the directory exists, create if not
            //if (!Directory.Exists(imagePath))
            //{
            //    Directory.CreateDirectory(imagePath);
            //}

            //// Combine the directory path with the file name
            //var fullFilePath = Path.Combine(imagePath, productDto.Image.FileName);

            //using (var stream = new FileStream(fullFilePath, FileMode.Create))
            //{
            //    await productDto.Image.CopyToAsync(stream);
            //}

            await _publishEndpoint.Publish(new ProductCreatedMessage
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImageUrl = newFilePath,
                Price = productDto.Price,
                InventoryCount = productDto.InventoryCount,
            });

            return Ok();
        }

        private async Task<string> updloadImage(IFormFile file)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images");

            // Check if the directory exists, create if not
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            // Combine the directory path with the file name
            var fullFilePath = Path.Combine(imagePath, file.FileName);

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{file.FileName}";
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto updatedProduct)
        {
            if (id != updatedProduct.Id) return BadRequest();


            string? newFilePath = null;

            if (updatedProduct.Image != null)
            {
                newFilePath = await updloadImage(updatedProduct.Image);
            }

            //_itemRepository.UpdateItem(updatedItem);
            //return NoContent();

            await _publishEndpoint.Publish(new ProductUpdateMessage
            {
                Id = id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                InventoryCount = updatedProduct.InventoryCount,
                ImageUrl = newFilePath,
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
