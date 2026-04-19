using eHotelMartinez.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<RoomImgData> Images { get; set; } = new();

        [Required]
        public decimal Price { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
    }
}
