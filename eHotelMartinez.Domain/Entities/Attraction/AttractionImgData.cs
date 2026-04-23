using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eHotelMartinez.Domain.Entities.Attraction
{
    public class AttractionImgData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        public string Url { get; set; } = null!;

        [Required]
        public AttractionData Attraction { get; set; } = null!;
    }
}
