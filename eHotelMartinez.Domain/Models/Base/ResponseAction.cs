using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Models.Base
{
    public class ResponseAction
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? Id { get; set; }
    }
}