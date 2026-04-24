using eHotelMartinez.Domain.Entities.Product;

namespace eHotelMartinez.Domain.Models.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ProductImgData> Images { get; set; } = new();
        public decimal Price { get; set; }
    }
}
