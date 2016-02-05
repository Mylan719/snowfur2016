using AutoMapper;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Mailer;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public class LoginFacade : ApplicationFacadeBase
    {
        public Func<ApplicationUserManager> AppUserManager { get; set; }

        public MailerService MailerService { get; set; }

        public ClaimsIdentity Login(string userName, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.Find(userName, password);

                if (user == null)
                {
                    return null;
                }

                return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
        }

        public int RegisterUser(AccountInfoDto newUser, string registrationUrlPrefix)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByName(newUser.UserName);

                if (user != null)
                {
                    throw new UIException("Užívateľ s týmo nickom už existuje.");
                }

                if (userManager.FindByEmail(newUser.Email) != null)
                {
                    throw new UIException("Užívateľ s týmo mailom už existuje.");
                }
                // create the user
                user = new User()
                {
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                };

                userManager.Create(user);

                uow.Commit();

                // send registration e-mail
                var registrationUrl = registrationUrlPrefix + "?username=" + newUser.UserName + "&token=" + userManager.GeneratePasswordResetToken(user.Id);
                MailerService.SendNewAccountEmail(newUser.Email, registrationUrl);

                return user.Id;
            }
        }

        public void SendPasswordResetEmail(string email, string registrationUrlPrefix)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByEmail(email);
                if (user == null)
                {
                    throw new UIException("Táto mailová adresa nepatrí k žiadnemu účtu!");
                }

                // send registration e-mail
                var registrationUrl = registrationUrlPrefix + "?username=" + user.UserName + "&token=" + userManager.GeneratePasswordResetToken(user.Id);

                MailerService.SendPasswordResetEmail(user.Email, registrationUrl);

            }
        }

        public void SetPassword(string userName, string newPassword, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByName(userName);
                if (user == null || !userManager.ResetPassword(user.Id, token, newPassword).Succeeded)
                {
                    throw new UIException("Odkaz na ktorý ste klikli už nie je aktuálny.");
                }
                user.EmailConfirmed = true;

                uow.Commit();
            }

        }

        public void CompleteRegistration(string userName, string newPassword, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByName(userName);
                if (user == null || !userManager.ResetPassword(user.Id, token, newPassword).Succeeded)
                {
                    throw new UIException("Odkaz na ktorý ste klikli vypršal. Registráciu dokončíte tak, že zvolíte \"Obnova hesla\" v hornom menu. Do poľa e-mail zadajté e-mail ktorý ste použili v prvom kroku registrácie. Bude vám poslaný mail. Kliknutím na odkaz v ňom môžete pokračovať v registrácií. Ak ponecháte registráciu nedokončenú zmaže sa. V tom prípade sa registrujte odznova.");
                }
                user.EmailConfirmed = true;

                uow.Commit();
            }

        }

        public AccountInfoDto GetUserInfo(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindById(id);
                return user == null ? null : Mapper.Map<AccountInfoDto>(user);
            }
        }

        public AccountInfoDto GetUserInfo(string contactEmail)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByName(contactEmail);
                return user == null ? null : Mapper.Map<AccountInfoDto>(user);
            }
        }

        public int? RegisterUserSilent(string contactEmail, string userName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = new User() { Email = contactEmail, UserName = userName };
                var result = userManager.Create(user);
                uow.Commit();

                if (!result.Succeeded)
                {
                    throw new UIException(string.Join(Environment.NewLine, result.Errors));
                }

                return user.Id;
            }
        }

        public void UpdateUserInfo(int currentUserId, AccountInfoDto userInfo)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindById(currentUserId);
                Mapper.Map(userInfo, user);
                uow.Commit();
            }
        }

        public void ChangePassword(int currentUserId, ChangePasswordDto passwordChange)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var result = userManager.ChangePassword(currentUserId, passwordChange.OldPassword, passwordChange.NewPassword);

                if (!result.Succeeded)
                {
                    if (result.Errors.Any(message => message.Contains("Incorrect password")))
                    {
                        throw new UIException("Nesprávne heslo.");
                    }

                    throw new UIException(string.Join(Environment.NewLine, result.Errors));
                }

                uow.Commit();
            }
        }
    }
}
