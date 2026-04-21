using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Models.Product
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }

            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }

            public List<string>? AddImages { get; set; }
            public List<int>? RemoveImageById { get; set; }
            public bool ClearImages { get; set; }
    }
}
