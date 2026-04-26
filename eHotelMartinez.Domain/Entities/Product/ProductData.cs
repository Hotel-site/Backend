using eHotelMartinez.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Product
{
    public class ProductData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        [Required]
        [Display(Name = "ProductName")]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string? Description { get; set; }

        public List<ProductImgData> Images { get; set; } = new();

        [Required]
        public decimal Price  { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Unknown;

        [Required]
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
