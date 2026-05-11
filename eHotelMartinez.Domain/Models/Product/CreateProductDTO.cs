
using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public List<ProductImageDTO>? Images { get; set; } = new();
        public int Stock { get; set; }
        public bool RequireBooking { get; set; }
        public ProductStatus ProductStatus { get; set; } = ProductStatus.Active;

    }
}