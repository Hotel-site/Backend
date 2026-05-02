using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Attraction
{
    public class AttractionImageData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        public string Url { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
