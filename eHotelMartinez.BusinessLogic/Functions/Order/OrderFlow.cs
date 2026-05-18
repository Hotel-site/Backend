using eHotelMartinez.BusinessLogic.Core.Order;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Order;

namespace eHotelMartinez.BusinessLogic.Functions.Order
{
    public class OrderFlow : OrderActions, IOrderActions
    {
        public async Task<ResponseMsg> ResponseAddToCartAction(int userId, OrderItemDTO item, decimal price)
        {
            return await ExecuteAddToCartAction(userId, item, price);
        }
        public async Task<OrderDTO> GetCartAction(int userId)
        {
            return await ExecuteGetCartAction(userId);
        }
        public async Task<List<OrderDTO>> GetOrderHistoryAction(int userId)
        {
            return await ExecuteGetOrderHistoryAction(userId);
        }
        public async Task<ResponseMsg> ResponseCheckoutAction(int userId)
        {
            return await ExecuteCheckoutAction(userId);
        }
        public async Task<ResponseMsg> ResponseRemoveFromCartAction(int orderItemId)
        {
            return await ExecuteRemoveFromCartAction(orderItemId);
        }
        public async Task<ResponseMsg> ResponseUpdateCartItemQuantityAction(int orderItemId, int quantity)
        {
            return await ExecuteUpdateCartItemQuantityAction(orderItemId, quantity);
        }
    }
}
