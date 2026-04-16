using eHotelMartinez.Domain.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductDTOController : ControllerBase
    {

        public static List<ProductDTO> _product = new();
        public static int _nextId = 1;


        [HttpGet("all")]
        public IActionResult GetAllProductDTOs()
        {
            return Ok(_product);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductDTOById(int id)
        {
            var product = _product.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"ProductDTO with ID {id} Not Found!" });
            }
            return Ok(product);
        }


        [HttpPost]
        public IActionResult CreateProductDTO([FromBody] ProductDTO product)
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
        public IActionResult UpdateProductDTO(int id, [FromBody] ProductDTO updatedProductDTO)
        {
            var existProductDTO = _product.FirstOrDefault(p => p.Id == id);

            if (existProductDTO == null)
            {
                return NotFound(new { Message = $"ProductDTO with ID {id} Not Found!" });
            }

            existProductDTO.Name = updatedProductDTO.Name;
            existProductDTO.Price = updatedProductDTO.Price;
            existProductDTO.Description = updatedProductDTO.Description;
            return Ok(existProductDTO);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteProductDTO(int id)
        {
            var product = _product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { Message = $"ProductDTO with ID {id} Not Found!" });
            }

            _product.Remove(product);
            return NoContent();
        }
    }
}
