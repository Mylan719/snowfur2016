using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Dtos;
using SnowFur.BL.Mailer;
using SnowFur.BL.Services;

namespace SnowFur.BL.Facades
{
    public class AccountFacade : ApplicationFacadeBase
    {
        private readonly UserService userService;
        private readonly PersonalProfileService personalProfileService;
        private readonly MailerService mailerService;

        public AccountFacade(UserService userService, PersonalProfileService personalProfileService, MailerService mailerService)
        {
            this.userService = userService;
            this.personalProfileService = personalProfileService;
            this.mailerService = mailerService;
        }

        public void ChangePassword(int currentUserId, ChangePasswordDto changePasswordDto)
            => userService.ChangePassword(currentUserId, changePasswordDto);

        public void SendPasswordResetEmail(string email, string url)
        {
            var userDto = userService.GetUserPasswordReset(email);
            mailerService.SendPasswordResetEmail(userDto.Email, $"{url}?username={userDto.UserName}&token={userDto.PasswordResetToken}");
        }

        public void SetPassword(string userName, string newPassword, string passwordToken)
            => userService.SetPassword(userName, newPassword, passwordToken);

        public ClaimsIdentity Login(string userName, string password)
            => userService.Login(userName, password);

        public void RegisterUser(AccountInfoDto account, string url)
        {
            var userPasswordResetDto = userService.RegisterUser(account);
            mailerService.SendPasswordResetEmail(userPasswordResetDto.Email, $"{url}?username={userPasswordResetDto.UserName}&token={userPasswordResetDto.PasswordResetToken}");
        }

        public void CompleteRegistration(string userName, string newPasswordPassword, string passwordToken, PersonalProfileDto profile)
        {
            var userInfo = userService.CompleteRegistration(userName, newPasswordPassword, passwordToken);
            personalProfileService.Insert(profile, userInfo.Id);
        }
    }
}
