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
        private readonly AccountFacade accountFacade; 

        public Register(AccountFacade accountFacade) : base()
        {
            SubpageTitle = "Registrácia";
            RabitBackgroundCssClass = "sf-bg-register";

            this.accountFacade = accountFacade;
        }

        public AccountInfoDto Account { get; set; } = new AccountInfoDto();

        [ModelValidationFilter]
        public void Submit ()
        {
            try
            {
                accountFacade.RegisterUser(Account, Context.Configuration.RouteTable["RegisterFinish"].Url);
                IsSuccessfullyFinished = true;
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Operácia skonèila chybou. Detaily: {ex.Message}";
            }
        }

    }
}
