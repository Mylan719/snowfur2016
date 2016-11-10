using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Mailer;
using SnowFur.BL.Queries;
using SnowFur.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public class MailingFacade
    {
        public MailerService MailerService { get; set; }
        public UserService UserService { get; set; }
     
        public void SendAdminBroadcastMail(string subject, string message)
        {
            var userMails = UserService.GetUserMailsForBroadcas();
            var failedUserNamesList = UserService.GetUserNamesWithUnconfirmedMails();

            foreach (var userMail in userMails)
            {
                try
                {
                    MailerService.SendAdminBroadcastMail(userMail.Email, subject, message);
                }
                catch (Exception)
                {
                    failedUserNamesList.Add(userMail.UserName);
                }
            }

            if (failedUserNamesList.Count > 0)
            {
                throw new UIException($"Nasledujúcim užívateľom sa nepodarilo poslať mail: {string.Join<string>(", ", failedUserNamesList)}");
            }
        }

    }
}
