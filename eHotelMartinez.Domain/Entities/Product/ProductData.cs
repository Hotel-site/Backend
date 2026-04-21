using eHotelMartinez.Domain.Entities.Room;
using eHotelMartinez.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Entities.Product
{
    public class ProductData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id  { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public List<ProductImgData> Images { get; set; } = new();

        [Required]
        public decimal Price  { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Unknown;

        public bool IsActive { get; set; }
    }
}
