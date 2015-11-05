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
            //var stringBuilder = new StringBuilder();
            //stringBuilder.Append(value: "<html><body style='font-family:calibri;'><p style='font-size:14px;'>Hi ,</p>");
            //stringBuilder.Append(value: "<br/><p style='font-size:13px;font-weight:bold;color:rgb(54,101,207)'>CUSTOMER CONTACT DETAIL</p>");
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.User, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.Mobile, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.Date, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.Customer, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.City, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: details.Address, str2: "</span></li></ul></div>"));
            //stringBuilder.Append(value: "<br/><br/><hr/>");
            //stringBuilder.Append(value: "<br/><br/>");
            //stringBuilder.Append(value: "<p>Wor ID:<span style='color:gray'> "+ details.WorkOrder + "</span> </p>");
            //stringBuilder.Append(value: "</body></html>");
            string extract = "Following details are extract from PDF";
            extract += "\n" + ("Work Order:" + details.WorkOrder);
            extract += "\n" + ("User:" + details.User);
            extract += "\n" + ("Date:" + details.Date);
            extract += "\n" + ("Customer:" + details.Customer);
            extract += "\n" + ("Mobile:" + details.Mobile);
            extract += "\n" + ("City:" + details.City);
            extract += "\n" + ("Address:" + details.Address);
            extract += "\n" + ("PinCode:" + details.PinCode);
            return extract;
        }
    }
}
