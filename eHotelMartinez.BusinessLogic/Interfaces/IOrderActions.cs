using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Order;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IOrderActions
    {
        Task<ResponseMsg> ResponseAddToCartAction(int userId, OrderItemDTO item, decimal price);
        Task<OrderDTO> GetCartAction(int userId);
        Task<List<OrderDTO>> GetOrderHistoryAction (int userId);
        Task<ResponseMsg> ResponseCheckoutAction(int userId);
        Task<ResponseMsg> ResponseRemoveFromCartAction(int orderItemId);
        Task<ResponseMsg> ResponseUpdateCartItemQuantityAction(int orderItemId, int quantity);
    }
}