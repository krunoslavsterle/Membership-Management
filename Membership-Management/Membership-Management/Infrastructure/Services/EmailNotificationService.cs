using Membership_Management.Infrastructure.Helpers;
using System;

namespace Membership_Management.Services
{
    public class EmailNotificationService
    {
        private DatabaseService databaseService = new DatabaseService();

        public void CheckSubscriptionsAndSendNotifications()
        {
            var smtpSettings = databaseService.GetSMTPSettings();
            if (!smtpSettings.Enabled)
                return;

            var customersToNotify = databaseService.FindCustomers(p =>
            p.ValidUntil < DateTime.Now.AddDays(5)
            && p.ValidUntil >= DateTime.Now
            && p.EmailNofificationMonth != DateTime.Now.Month
            && !String.IsNullOrEmpty(p.Email));

            foreach (var customer in customersToNotify)
            {
                try
                {
                    if (EmailHelper.SendEmail(smtpSettings, customer.Email))
                    {
                        customer.EmailNofificationMonth = DateTime.Now.Month;
                        databaseService.UpdateCustomer(customer);
                    }
                }
                catch {}
            }
        }
    }
}
