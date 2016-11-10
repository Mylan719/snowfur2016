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
        private readonly AccountFacade accountFacade;

        [Required(ErrorMessage = "Email nezadany.")]
        [EmailAddress(ErrorMessage = "Email v nesprávnom tvare.")]
        public string Email { get; set; }

        public PasswordRecoveryStep1(AccountFacade accountFacade) : base()
        {
            SubpageTitle = "Obnova hesla";
            RabitBackgroundCssClass = "sf-bg-password-recovery";

            this.accountFacade = accountFacade;
        }

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                accountFacade.SendPasswordResetEmail(Email, Context.Configuration.RouteTable["PasswordRecoveryStep2"].Url);
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
