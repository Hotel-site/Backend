using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductActions _productActions;
        public ProductController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _productActions = bl.GetProductActions();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productActions.GetAllProductsAction();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productActions.GetProductByIdAction(id);

            if (product == null)
            {
                return NotFound(new
                {
                    Message = $"Product with ID {id} Not Found!"
                });
            }
            return Ok(product);
        }

        [AdminOnly]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
        {

            var response = await _productActions.ResponseProductCreateAction(product);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [AdminOnly]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO product)
        {

            product.Id = id;
            var response = await _productActions.ResponseProductUpdateAction(product);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [AdminOnly]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var response = await _productActions.ResponseProductDeleteAction(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
