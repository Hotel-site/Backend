using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Amenities { get; set; } = new();
        public List<RoomImageDTO> Images { get; set; } = new();
        public decimal Price { get; set; }
        public RoomStatus Status { get; set; }
    }
}
