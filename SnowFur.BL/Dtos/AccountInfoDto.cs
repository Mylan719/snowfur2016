using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public class AccountInfoDto
    {
        [Required(ErrorMessage = "Zadajte prezívku")]
        [RegularExpression(@"^[\p{L}\d \-]+$", ErrorMessage = "Údaj v neočakávanom tvare.")]
        [MaxLength(50, ErrorMessage = "Prezívka musí mať menej ako 50 znakov.")]
        public string UserName { get; set; }

        [MaxLength(200, ErrorMessage = "Maximálna dĺžka je 200 znakov.")]
        [Required(ErrorMessage = "Zadajte email")]
        [EmailAddress(ErrorMessage = "Email nemá správny tvar")]
        public string Email { get; set; }
    }
}
