namespace eHotelMartinez.Domain.Models.Attraction
{
    public class OpeningHourDTO
    {
        public Enums.DayOfWeek DayOfWeek { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
