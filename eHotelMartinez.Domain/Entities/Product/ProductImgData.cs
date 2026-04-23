using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Product
{
    public class ProductImgData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public ProductData Product { get; set; } = null!;
    }
}
