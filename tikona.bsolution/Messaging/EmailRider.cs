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
        public void Send(RecoveryDetails details)
        {
            try
            {
                var mailMessage = new MailMessage(from: "info@bsolution.in", to: "gplakrish@gmail.com")
                {
                    Subject = "Work Order",
                    Body = FormMessage(details),
                    IsBodyHtml = false
                };
                mailMessage.Bcc.Add(addresses: "arun.seetharaman8@gmail.com");
                mailMessage.Bcc.Add(addresses: "kumarjbram@yahoo.com");
                var smtpClient = new SmtpClient(host: "mail.bsolution.in", port: 25);
                var networkCredential = new NetworkCredential()
                {
                    UserName = "info@bsolution.in",
                    Password = "Payoda@123"
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

        private string FormMessage(RecoveryDetails details)
        {
            string extract = "Following details are extracted from PDF";
            extract += "\n" + ("Work Order:" + details.WorkOrder);
            extract += "\n" + ("User      :" + details.User);
            extract += "\n" + ("Date      :" + details.Date);
            extract += "\n" + ("Customer  :" + details.Customer);
            extract += "\n" + ("Mobile    :" + details.Mobile);
            extract += "\n" + ("City      :" + details.City);
            extract += "\n" + ("Address   :" + details.Address);
            extract += "\n" + ("PinCode   :" + details.PinCode);
            return extract;
        }
    }
}
