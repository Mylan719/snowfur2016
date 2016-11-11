using AutoMapper;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Mailer;
using SnowFur.BL.Queries;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Services
{
    public class UserService : ApplicationServiceBase
    {
        public Func<ApplicationUserManager> AppUserManager { get; set; }
        public Func<UserEmailsQuery> UserEmailQueryFunc { get; set; }

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

        public UserPasswordResetDto RegisterUser(AccountInfoDto newUser)
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
                    DateCreated = DateTime.UtcNow
                };

                userManager.Create(user);

                uow.Commit();

                var userDto = Mapper.Map<UserPasswordResetDto>(user);
                userDto.PasswordResetToken = userManager.GeneratePasswordResetToken(user.Id);
                return userDto;
            }
        }

        public UserPasswordResetDto GetUserPasswordReset(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByEmail(email);
                if (user == null)
                {
                    throw new UIException("Táto mailová adresa nepatrí k žiadnemu účtu!");
                }
                var dto = Mapper.Map<UserPasswordResetDto>(user);
                dto.PasswordResetToken = userManager.GeneratePasswordResetToken(user.Id);
                return dto;
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
                user.DateUpdated = DateTime.UtcNow;;

                uow.Commit();
            }

        }

        public UserBasicInfoDto CompleteRegistration(string userName, string newPassword, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = userManager.FindByName(userName);
                if (user == null || !userManager.ResetPassword(user.Id, token, newPassword).Succeeded)
                {
                    throw new UIException(
                        "Odkaz na ktorý ste klikli vypršal. Registráciu dokončíte tak, že zvolíte \"Obnova hesla\" v hornom menu. Do poľa e-mail zadajté e-mail ktorý ste použili v prvom kroku registrácie. Bude vám poslaný mail. Kliknutím na odkaz v ňom môžete pokračovať v registrácií. Ak ponecháte registráciu nedokončenú zmaže sa. V tom prípade sa registrujte odznova.");
                }
                user.EmailConfirmed = true;

                uow.Commit();
                return Mapper.Map<UserBasicInfoDto>(user);
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

        public int? RegisterUserSilent(string contactEmail, string userName)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var user = new User() {Email = contactEmail, UserName = userName, DateCreated = DateTime.UtcNow};
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
                user.DateUpdated = DateTime.UtcNow;
                uow.Commit();
            }
        }

        public void ChangePassword(int currentUserId, ChangePasswordDto passwordChange)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var userManager = AppUserManager();
                var result = userManager.ChangePassword(currentUserId, passwordChange.OldPassword,
                    passwordChange.NewPassword);

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

        public List<UserEmailDto> GetUserMailsForBroadcas()
        {
            using (UnitOfWorkProvider.Create())
            {
                return UserEmailQueryFunc().Execute().ToList();
            }
        }

        public List<string> GetUserNamesWithUnconfirmedMails()
        {
            using (UnitOfWorkProvider.Create())
            {
                var unconfirmedMailsQuery = UserEmailQueryFunc();
                unconfirmedMailsQuery.Filter = new UserEmailFilter {MailConfirmed = false, MailUnconfirmed = true};

                return unconfirmedMailsQuery.Execute().Select(dto => dto.UserName).ToList();
            }
        }
    }
}
