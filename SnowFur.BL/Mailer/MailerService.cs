using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace SnowFur.BL.Mailer
{
    public class MailerService
    {

        public string Domain
        {
            get { return ConfigurationManager.AppSettings["WebsiteDomain"]; }
        }

        public string AdminEmail
        {
            get { return ConfigurationManager.AppSettings["AdminEmail"]; }
        }


        public void SendNewAccountEmail(string email, string registrationUrl)
        {
            var body = new NewAccountEmail();
            body.RegistrationUrl = Domain + registrationUrl;
            SendMail(email, "Nová registrácia", body.TransformText());
        }

        public void SendPasswordResetEmail(string email, string recoveryUrl)
        {
            var body = new PasswordResetEmail();
            body.RecoveryUrl = Domain + recoveryUrl;
            SendMail(email, "Obnova zabudnutého hesla", body.TransformText());
        }

        public void SendAdminBroadcastMail(string email, string subject, string message)
        {
            var body = new BroadcastMail();
            body.Subject = subject;
            body.MessageLines = message.Split('\n').Select( line => line.TrimEnd() ).ToList();

            SendMail(email, subject, body.TransformText());
        }

        private void SendMail(string to, string subject, string body, string replyTo = null, bool isBodyHtml = true, Attachment[] attachments = null)
        {
            using (var smtp = new SmtpClient())
            using (var message = new MailMessage())
            {
                message.To.Add(to);

                if (replyTo != null)
                {
                    message.ReplyToList.Add(replyTo);
                }

                message.Subject = "Snowfur 2019: " + subject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = body;

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                }

                smtp.Send(message);
            }
        }
    }
}
