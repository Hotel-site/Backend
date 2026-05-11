using System.Text.Json.Serialization;
using eHotelMartinez.Domain.Enums;

namespace eHotelMartinez.Domain.Models.Favorite
{
    public class FavoriteDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public  EntityType EntityType{ get; set; }
    }
}
