namespace eHotelMartinez.Domain.Models.Restaurant
{
    public class DishDTO
    {
        public int Id { get; set; }
        public Enums.DayOfWeek DayOfWeek { get; set; }
        public Enums.Meals Meal { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
