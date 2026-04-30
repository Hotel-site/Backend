using eHotelMartinez.Domain.Entities.Category;

namespace eHotelMartinez.Domain.Models.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public List<ProductImgDTO> Images { get; set; } = new();
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
