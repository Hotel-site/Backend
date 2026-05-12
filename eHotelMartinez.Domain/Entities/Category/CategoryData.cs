using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eHotelMartinez.Domain.Entities.Category
{
    public class CategoryData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}