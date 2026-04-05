namespace eHotelMartinez.Api.Domain
{
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { set; get; } = string.Empty;
        public decimal Price { set; get; }
        public string Description { set; get; } = string.Empty;

    }
}