using Membership_Management.Infrastructure.Models;
using System.Net;
using System.Net.Mail;

namespace Membership_Management.Infrastructure.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(SMTPSettings smtpSettings, string email)
        {
            if (smtpSettings == null || !smtpSettings.Enabled)
                return false;

            var smptClient = new SmtpClient(smtpSettings.ServerName, smtpSettings.Port)
            {
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                EnableSsl = true
            };

            var message = new MailMessage(smtpSettings.Username, email);
            message.Subject = smtpSettings.EmailSubject;
            message.Body = smtpSettings.EmailBody;
            smptClient.Send(message);
            return true;
        }
    }
}
