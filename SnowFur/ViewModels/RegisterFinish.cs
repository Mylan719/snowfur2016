using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Dtos;
using DotVVM.Framework.Runtime.Filters;
using System;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Facades;
using AutoMapper;

namespace SnowFur.ViewModels
{
    public class RegisterFinish : PageViewModelBase
    {
        private LoginFacade loginFacade;
        private PersonalProfileFacade personalProfileFacade;

        public RegisterFinish(LoginFacade loginFacade, PersonalProfileFacade personalProfileFacade) : base()
        {
            SubpageTitle = "Dokončenie registrácie";
            RabitBackgroundCssClass = "sf-bg-register";

            this.loginFacade = loginFacade;
            this.personalProfileFacade = personalProfileFacade;
        }

        [Bind(Direction.None)]
        public string PasswordToken => GetQuerySafe<string>("token");

        [Bind(Direction.None)]
        public string UserName => GetQuerySafe<string>("username");

        public NewPasswordDto NewPassword { get; set; } = new NewPasswordDto();

        public PersonalProfileForm Profile { get; set; }

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                loginFacade.CompleteRegistration(UserName, NewPassword.Password, PasswordToken);
                personalProfileFacade.Insert(Mapper.Map<PersonalProfileDto>(Profile), UserName);
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            var identity = loginFacade.Login(UserName, NewPassword.Password);
            if (identity != null)
            {
                Context.OwinContext.Authentication.SignIn(identity);
                Context.Redirect("MyProfile", null);
            }
            IsSuccessfullyFinished = true;
        }

    }
}
