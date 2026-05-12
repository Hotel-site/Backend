using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Attraction
{
    public class OpeningHourData
    {
        public int Id { get; set; }
        public int AttractionId { get; set; }

        [ForeignKey("AttractionId")]
        public AttractionData Attraction { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public bool IsActive { get; set; } = true;
    }
}