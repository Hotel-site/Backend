using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Entities.Room;
using eHotelMartinez.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Models.Room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Amenities { get; set; } = new();
        public List<RoomImgData> Images { get; set; } = new();
        public decimal Price { get; set; }
    }
}
