using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
