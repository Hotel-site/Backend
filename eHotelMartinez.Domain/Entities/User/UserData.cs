using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHotelMartinez.Domain.Entities.User
{
    public class UserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegisteredOn { get; set; }
        public bool IsActive { get; set; }

    }
}
