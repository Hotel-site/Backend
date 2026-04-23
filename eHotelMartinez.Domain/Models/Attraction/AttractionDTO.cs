using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.Domain.Entities.Attraction;

namespace eHotelMartinez.Domain.Models.Attraction
{
    public class AttractionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int Popularity { get; set; }
        public List<AttractionImgData> Images { get; set; } = new();

    }
}
