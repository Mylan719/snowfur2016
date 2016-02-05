using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Dtos;
using DotVVM.Framework.Runtime.Filters;
using System;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Facades;

namespace SnowFur.ViewModels
{
    public class Register : PageViewModelBase
    {
        private LoginFacade loginFacade; 

        public Register(LoginFacade loginFacade) : base()
        {
            SubpageTitle = "Registrácia";
            RabitBackgroundCssClass = "sf-bg-register";

            this.loginFacade = loginFacade;
        }

        public AccountInfoDto Account { get; set; } = new AccountInfoDto();

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                loginFacade.RegisterUser(Account, Context.Configuration.RouteTable["RegisterFinish"].Url);
                IsSuccessfullyFinished = true;
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }

    }
}
