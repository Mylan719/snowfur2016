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
        private AccountFacade accountFacade;
        private PersonalProfileFacade personalProfileFacade;

        public RegisterFinish(AccountFacade accountFacade, PersonalProfileFacade personalProfileFacade) : base()
        {
            SubpageTitle = "Dokončenie registrácie";
            RabitBackgroundCssClass = "sf-bg-register";

            this.accountFacade = accountFacade;
            this.personalProfileFacade = personalProfileFacade;
        }

        [Bind(Direction.None)]
        public string PasswordToken => GetQueryOrDefault("token");

        [Bind(Direction.None)]
        public string UserName => GetQueryOrDefault("username");

        public NewPasswordDto NewPassword { get; set; } = new NewPasswordDto();

        public PersonalProfileForm Profile { get; set; }

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {         
                var profileDto = Mapper.Map<PersonalProfileDto>(Profile);
                accountFacade.CompleteRegistration(UserName, NewPassword.Password, PasswordToken, profileDto);
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            var identity = accountFacade.Login(UserName, NewPassword.Password);
            if (identity != null)
            {
                Authentication.SignIn(identity);
                Context.RedirectToRoute("MyProfile", null);
            }
            IsSuccessfullyFinished = true;
        }

    }
}
