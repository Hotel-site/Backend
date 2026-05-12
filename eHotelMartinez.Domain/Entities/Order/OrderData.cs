using eHotelMartinez.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Order
{
    public class OrderData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public decimal TotalSum { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [InverseProperty("Order")]
        public  List<OrderItemData> OrderItems { get; set; } = new();
    }
}
