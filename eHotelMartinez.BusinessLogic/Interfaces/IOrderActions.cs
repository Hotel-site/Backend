using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Order;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IOrderActions
    {
        ResponseMsg ResponseAddToCartAction(int userId, OrderItemDTO item, decimal price);
        OrderDTO GetCartAction(int userId);
        List<OrderDTO> GetOrderHistoryAction (int userId);
        ResponseMsg ResponseCheckoutAction(int userId);
        ResponseMsg ResponseRemoveFromCartAction(int orderItemId);
        ResponseMsg ResponseUpdateCartItemQuantityAction(int orderItemId, int quantity);
    }
}