using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace SnowFur.ViewModels.Controls
{
    public class PasswordChangeForm : OwinViewModelBase
    {
        [Required(ErrorMessage = "Zadajte pôvodné heslo.")]
        public string OldPassword { get; set; }

        [MinLength(6, ErrorMessage = "Heslo musí mať aspoň 6 znakov.")]
        [Required(ErrorMessage = "Zadajte nové heslo.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Zadajte nové heslo pre kontrolu.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Heslá sa musia zhodovať.")]
        public string NewPasswordAgain { get; set; }

        public void Clear() {
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            NewPasswordAgain = string.Empty;
        }
    }
}
