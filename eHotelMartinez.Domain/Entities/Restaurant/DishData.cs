using eHotelMartinez.Domain.Models.Restaurant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Restaurant
{
    public class DishData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Enums.DayOfWeek DayOfWeek { get; set; }
        [Required]
        public Enums.Meals Meal { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
