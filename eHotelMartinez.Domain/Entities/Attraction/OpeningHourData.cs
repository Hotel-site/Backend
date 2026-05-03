namespace eHotelMartinez.Domain.Entities.Attraction
{
    public class OpeningHourData
    {
        public int Id { get; set; }
        public int AttractionId { get; set; }
        public Enums.DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
    }
}
