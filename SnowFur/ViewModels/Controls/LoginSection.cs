using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using Microsoft.AspNet.Identity;
using SnowFur.BL.Facades;
using System.ComponentModel.DataAnnotations;

namespace SnowFur.ViewModels.Controls
{
    public class LoginSection : DotvvmViewModelBase
    {
        private LoginFacade loginFacade;

        [Bind(Direction.ServerToClient)]
        public string CurrentUserName => Context.OwinContext.Authentication.User?.Identity?.Name;

        [Bind(Direction.ServerToClient)]
        public int CurrentUserId => Context.OwinContext.Authentication.User?.Identity?.GetUserId<int>() ?? 0;

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

        public LoginSection(LoginFacade loginFacade) : base()
        {
            this.loginFacade = loginFacade;
        }

        public void Login()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Meno alebo heslo nezadané";
                return;
            }

            var identity = loginFacade.Login(UserName, Password);
            if (identity == null)
            {
                ErrorMessage = "Nesprávne meno a heslo.";
                return;
            }
            else
            {
                Context.OwinContext.Authentication.SignIn(identity);
            }
            Context.Redirect("MyProfile", null);
        }

        public void Logout()
        {
            Context.OwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Context.Redirect("Default", null);
        }
    }
}