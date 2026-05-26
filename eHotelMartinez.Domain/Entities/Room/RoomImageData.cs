using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Room
{
    public class RoomImageData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public RoomData Room { get; set; }

        [Required]
        public string Url { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
