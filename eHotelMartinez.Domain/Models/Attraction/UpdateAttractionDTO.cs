using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.ValueObjects;

namespace eHotelMartinez.Domain.Models.Attraction
{
    public class UpdateAttractionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Location Location { get; set; }
        public double Distance { get; set; }
        public decimal Price { get; set; }
        public List<AttractionImageDTO> Images { get; set; } = new();
        public List<OpeningHourDTO> OpeningHours { get; set; } = new();
        public PartnerContacts Contacts { get; set; }
        public bool IsActive { get; set; }
    }
}
