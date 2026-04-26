using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Enums;
using eHotelMartinez.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eHotelMartinez.Domain.Entities.Attraction
{
    public class AttractionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Description { get; set; }
        [StringLength(100)]
        public string? ShortDescription { get; set; }
        public CategoryData Category { get; set; }

        [Required]
        [StringLength(100)]
        public string? Location { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public decimal Price { get; set; }

        public double Rating { get; set; } = 0.0;

        public int Popularity { get; set; } = 0;

        public List<AttractionImgData> Images { get; set; } = new();

        public IReadOnlyDictionary<DayOfWeek, IReadOnlyList<(TimeOnly Start, TimeOnly End)>> OpeningHours;

        public PartnerContacts? Contacts { get; set; }

        public bool IsActive { get; set; }
    }
}
