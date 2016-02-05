using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Mailer;
using SnowFur.BL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public class MailingFacade : ApplicationFacadeBase
    {
        public MailerService MailerService { get; set; }
        public Func<UserEmailsQuery> UserEmailQueryFunc { get; set; }

        public void SendAdminBroadcastMail(string subject, string message)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var list = UserEmailQueryFunc().Execute();

                var unconfirmedMailsQuery = UserEmailQueryFunc();
                unconfirmedMailsQuery.Filter = new UserEmailFilter { MailConfirmed = false, MailUnconfirmed = true };

                var failedUserNamesList = unconfirmedMailsQuery.Execute().Select(dto => dto.UserName).ToList();

                foreach (var userMail in list)
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
                    throw new UIException(string.Format("Nasledujúcim užívateľom sa nepodarilo poslať mail: {0}", string.Join<string>(", ", failedUserNamesList)));
                }
            }
        }
    }
}
