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
        private LoginFacade loginFacade;
        private PersonalProfileFacade personalProfileFacade;

        public PasswordRecoveryStep2(LoginFacade loginFacade, PersonalProfileFacade personalProfileFacade) : base()
        {
            SubpageTitle = "Obnova hesla";
            RabitBackgroundCssClass = "sf-bg-password-recovery";

            this.loginFacade = loginFacade;
            this.personalProfileFacade = personalProfileFacade;
        }

        [Bind(Direction.None)]
        public string PasswordToken => GetQuerySafe<string>("token");

        [Bind(Direction.None)]
        public string UserName => GetQuerySafe<string>("username");

        public NewPasswordDto NewPassword { get; set; } = new NewPasswordDto();

        public override Task Init()
        {
            if( !personalProfileFacade.ProfileExists(UserName))
            {
                Context.Redirect(string.Format("~/registerFinish?username={0}&token={1}", UserName, PasswordToken));
            }
            return base.Init();
        }

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                loginFacade.SetPassword(UserName, NewPassword.Password, PasswordToken);
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
