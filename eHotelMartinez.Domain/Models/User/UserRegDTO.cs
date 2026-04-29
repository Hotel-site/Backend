using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Models.User
{
    public class UserRegDTO
    {
        public string Username {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}