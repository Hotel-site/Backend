using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private IOrderActions _orderActions;

        public OrderController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _orderActions = bl.GetOrderActions();
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var cart = _orderActions.GetCartAction(userId);
            if (cart == null)
            {
                return NotFound(new {Message = "Cart is empty!"});
            }
            return Ok(cart);
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetOrderHistory(int userId)
        {
            var history = _orderActions.GetOrderHistoryAction(userId);
            if (history == null)
            {
                return NotFound(new { Message = "History of Orders is empty"});
            }
            return Ok(history);
        }

        [HttpPost("cart/add")]
        public IActionResult AddToCart([FromBody] CartItemReq request)
        {
            var result = _orderActions.ResponseAddToCartAction(request.UserId, request.Item, request.Price);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("checkout/{userId}")]
        public IActionResult Checkout(int userId)
        {
            var result = _orderActions.ResponseCheckoutAction(userId);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("cart/item/{itemId}/quantity")]
        public IActionResult UpdateCartItemQuantity(int itemId, [FromBody] QuantityReq request)
        {
            var result = _orderActions.ResponseUpdateCartItemQuantityAction(itemId, request.Quantity);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("cart/item/{itemId}")]
        public IActionResult RemoveFromCart(int itemId)
        {
            var result = _orderActions.ResponseRemoveFromCartAction(itemId);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
