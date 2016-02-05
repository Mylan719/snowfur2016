using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public class NewPasswordDto
    {
        [MinLength(6, ErrorMessage = "Heslo musí mať aspoň 6 znakov.")]
        [Required(ErrorMessage = "Heslo nezadané")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zadajte heslo znovu pre kontrolu.")]
        [Compare(nameof(Password), ErrorMessage = "Heslá sa musia zhodovať")]
        public string PasswordAgain { get; set; }

    }
}
