using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Models.User
{
    public class UserActivateDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}