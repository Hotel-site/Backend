using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderItemType Type { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}