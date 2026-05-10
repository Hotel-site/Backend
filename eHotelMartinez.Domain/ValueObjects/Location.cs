using System.Globalization;

namespace eHotelMartinez.Domain.ValueObjects
{
    public class Location
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Location() { }
    }
}
