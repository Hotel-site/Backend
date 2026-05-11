using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Favorite
{
    public class CreateFavoriteDTO
    {
        public int UserId { get; set; }
        public EntityType EntityType { get; set; }
        public int EntityId { get; set; }
    }
}
