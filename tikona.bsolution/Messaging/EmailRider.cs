using Bns.Framework.Common.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bns.Framework.Common.Messaging
{
    public class EmailRider : IRider
    {
        public void Send(Dictionary<string, string> details)
        {
            try
            {
                var settings = SettingsManager.EmailRiderSettings;
                var mailMessage = new MailMessage(from: settings.From, to: settings.To)
                {
                    Subject = "Work Order",
                    Body = FormMessage(details),
                    IsBodyHtml = false
                };
                if (!string.IsNullOrEmpty(settings.Cc))
                    mailMessage.CC.Add(addresses: settings.Cc);
                if (!string.IsNullOrEmpty(settings.Bcc))
                    mailMessage.Bcc.Add(addresses: settings.Bcc);
                var smtpClient = new SmtpClient(settings.Host, settings.Port);
                var networkCredential = new NetworkCredential()
                {
                    UserName = settings.UserName,
                    Password = settings.Password
                };
                smtpClient.Credentials = networkCredential;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                ErrorQueue.Enqueue(ex.Message);
                throw;
            }
        }

        private string FormMessage(Dictionary<string, string> details)
        {
            string extract = "Following details are extracted from PDF";
            extract += "\n" + ("Work Order:" + details["WorkOrder"]);
            extract += "\n" + ("User      :" + details["User"]);
            extract += "\n" + ("Date      :" + details["Date"]);
            extract += "\n" + ("Customer  :" + details["Customer"]);
            extract += "\n" + ("Mobile    :" + details["Mobile"]);
            extract += "\n" + ("City      :" + details["City"]);
            extract += "\n" + ("Address   :" + details["Address"]);
            extract += "\n" + ("PinCode   :" + details["PinCode"]);
            return extract;
        }
    }
}
