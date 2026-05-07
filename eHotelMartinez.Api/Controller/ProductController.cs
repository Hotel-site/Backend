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
        public IActionResult GetAllProducts()
        {
            var products = _productActions.GetAllProductsAction();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productActions.GetProductByIdAction(id);

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
        public IActionResult CreateProduct([FromBody] CreateProductDTO product)
        {

            var response = _productActions.ResponseProductCreateAction(product);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [AdminOnly]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDTO product)
        {

            product.Id = id;
            var response = _productActions.ResponseProductUpdateAction(product);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [AdminOnly]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            var response = _productActions.ResponseProductDeleteAction(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
