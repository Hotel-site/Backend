using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Product
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ProductImgDTO>? Images { get; set; } = new();
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
