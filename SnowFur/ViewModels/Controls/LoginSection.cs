using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.AspNet.Identity;
using SnowFur.BL.Facades;
using System.ComponentModel.DataAnnotations;

namespace SnowFur.ViewModels.Controls
{
    public class LoginSection : OwinViewModelBase
    {
        private AccountFacade accountFacade;

        [Bind(Direction.ServerToClient)]
        public string CurrentUserName => Context.HttpContext.User?.Identity?.Name;

        [Bind(Direction.ServerToClient)]
        public int CurrentUserId => Context.HttpContext.User?.Identity?.GetUserId<int>() ?? 0;

        [Bind(Direction.ServerToClient)]
        public bool IsAuthenticated => CurrentUserId != 0;

        [Bind(Direction.ServerToClient)]
        public bool IsError => !string.IsNullOrEmpty(ErrorMessage);

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }

        [Bind(Direction.Both)]
        public string UserName { get; set; }

        [Bind(Direction.Both)]
        public string Password { get; set; }

        public LoginSection(AccountFacade accountFacade) : base()
        {
            this.accountFacade = accountFacade;
        }

        public void Login()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Meno alebo heslo nezadané";
                return;
            }

            var identity = accountFacade.Login(UserName, Password);
            if (identity == null)
            {
                ErrorMessage = "Nesprávne meno a heslo.";
                return;
            }
            else
            {
                Authentication.SignIn(identity);
            }
            Context.RedirectToRoute("MyProfile", null);
        }

        public void Logout()
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Context.RedirectToRoute("Default", null);
        }
    }
}