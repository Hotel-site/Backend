using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.Domain.Models.Product;

namespace eHotelMartinez.Domain.Models.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductImgDTO>? Images { get; set; } = new();
    }
}
