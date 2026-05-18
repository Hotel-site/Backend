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
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _orderActions.GetCartAction(userId);
            if (cart == null)
            {
                return NotFound(new {Message = "Cart is empty!"});
            }
            return Ok(cart);
        }

        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetOrderHistory(int userId)
        {
            var history = await _orderActions.GetOrderHistoryAction(userId);
            if (history == null)
            {
                return NotFound(new { Message = "History of Orders is empty"});
            }
            return Ok(history);
        }

        [HttpPost("cart/add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemReq request)
        {
            var result = await _orderActions.ResponseAddToCartAction(request.UserId, request.Item, request.Price);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(int userId)
        {
            var result = await _orderActions.ResponseCheckoutAction(userId);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("cart/item/{itemId}/quantity")]
        public async Task<IActionResult> UpdateCartItemQuantity(int itemId, [FromBody] QuantityReq request)
        {
            var result = await _orderActions.ResponseUpdateCartItemQuantityAction(itemId, request.Quantity);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("cart/item/{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var result = await _orderActions.ResponseRemoveFromCartAction(itemId);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
