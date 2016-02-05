using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Dtos;
using DotVVM.Framework.Runtime.Filters;
using System;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Facades;
using System.ComponentModel.DataAnnotations;

namespace SnowFur.ViewModels
{
    public class PasswordRecoveryStep1 : PageViewModelBase
    {
        private LoginFacade loginFacade;

        [Required(ErrorMessage = "Email nezadany.")]
        [EmailAddress(ErrorMessage = "Email v nesprávnom tvare.")]
        public string Email { get; set; }

        public PasswordRecoveryStep1(LoginFacade loginFacade) : base()
        {
            SubpageTitle = "Obnova hesla";
            RabitBackgroundCssClass = "sf-bg-password-recovery";

            this.loginFacade = loginFacade;
        }

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                loginFacade.SendPasswordResetEmail(Email, Context.Configuration.RouteTable["PasswordRecoveryStep2"].Url);
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            IsSuccessfullyFinished = true;
        }

    }
}
