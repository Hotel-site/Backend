using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Room
{
    public class RoomData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]  
        [StringLength(400)]
        public string Description { get; set; }

        public List<string> Amenities { get; set; } = new();

        [InverseProperty("Room")]
        public List<RoomImageData> Images { get; set; } = new();

        [Required]
        public decimal Price { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
    }
}
