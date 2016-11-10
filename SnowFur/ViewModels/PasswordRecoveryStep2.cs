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
    public class PasswordRecoveryStep2 : PageViewModelBase
    {
        private readonly AccountFacade accountFacade;
        private readonly PersonalProfileFacade personalProfileFacade;

        public PasswordRecoveryStep2(AccountFacade accountFacade, PersonalProfileFacade personalProfileFacade) : base()
        {
            SubpageTitle = "Obnova hesla";
            RabitBackgroundCssClass = "sf-bg-password-recovery";

            this.accountFacade = accountFacade;
            this.personalProfileFacade = personalProfileFacade;
        }

        [Bind(Direction.None)]
        public string PasswordToken => GetQueryOrDefault("token");

        [Bind(Direction.None)]
        public string UserName => GetQueryOrDefault("username");

        public NewPasswordDto NewPassword { get; set; } = new NewPasswordDto();

        public override Task Init()
        {
            if( !personalProfileFacade.ProfileExists(UserName))
            {
                Context.RedirectToRoute($"~/registerFinish?username={UserName}&token={PasswordToken}");
            }
            return base.Init();
        }

        [ModelValidationFilter]
        public void Submit()
        {
            try
            {
                accountFacade.SetPassword(UserName, NewPassword.Password, PasswordToken);
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
