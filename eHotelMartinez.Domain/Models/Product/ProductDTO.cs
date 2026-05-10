using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public List<ProductImageDTO> Images { get; set; } = new();
        public int Stock { get; set; }
        public bool RequireBooking { get; set; }
        public ProductStatus Status { get; set; }
    }
}