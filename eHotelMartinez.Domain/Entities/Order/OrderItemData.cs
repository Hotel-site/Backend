using eHotelMartinez.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Order
{
    public class OrderItemData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public OrderData Order { get; set; }

        [Required]
        public OrderItemType Type { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceAtPurchase { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
