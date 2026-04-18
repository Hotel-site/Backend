using eHotelMartinez.Domain.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<ProductDTO> _product = new();
        public static int _nextId = 1;


        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_product);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _product.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} Not Found!" });
            }
            return Ok(product);
        }


        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            if (product.Name == null || product.Name == "")
            {
                return BadRequest(new { Message = "Name is empty!" });
            }
            product.Id = _nextId++;
            

            _product.Add(product);

            return Created($"/api/products/{product.Id}", product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO updatedProduct)
        {
            var existProduct = _product.FirstOrDefault(p => p.Id == id);

            if (existProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} Not Found!" });
            }

            existProduct.Name = updatedProduct.Name;
            existProduct.Price = updatedProduct.Price;
            existProduct.Description = updatedProduct.Description;
            return Ok(existProduct);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} Not Found!" });
            }

            _product.Remove(product);
            return NoContent();
        }
    }
}
