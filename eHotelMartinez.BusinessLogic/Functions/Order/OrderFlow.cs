using eHotelMartinez.BusinessLogic.Core.Order;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Order;

namespace eHotelMartinez.BusinessLogic.Functions.Order
{
    public class OrderFlow : OrderActions, IOrderActions
    {
        public ResponseMsg ResponseAddToCartAction(int userId, OrderItemDTO item, decimal price)
        {
            return ExecuteAddToCartAction(userId, item, price);
        }
        public OrderDTO GetCartAction(int userId)
        {
            return ExecuteGetCartAction(userId);
        }
        public List<OrderDTO> GetOrderHistoryAction(int userId)
        {
            return ExecuteGetOrderHistoryAction(userId);
        }
        public ResponseMsg ResponseCheckoutAction(int userId)
        {
            return ExecuteCheckoutAction(userId);
        }
        public ResponseMsg ResponseRemoveFromCartAction(int orderItemId)
        {
            return ExecuteRemoveFromCartAction(orderItemId);
        }
        public ResponseMsg ResponseUpdateCartItemQuantityAction(int orderItemId, int quantity)
        {
            return ExecuteUpdateCartItemQuantityAction(orderItemId, quantity);
        }
    }
}
