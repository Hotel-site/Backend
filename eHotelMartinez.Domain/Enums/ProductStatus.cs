using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Enums
{
    public enum ProductStatus
    {
        Unknown = 0,
        Active = 1,
        Inactive = 2,
        OutOfStock = 3,
        Discontinued = 4
    }
}
