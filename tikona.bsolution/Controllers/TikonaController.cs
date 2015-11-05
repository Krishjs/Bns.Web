using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Tikona.Bsolution.Controllers
{
    public class TikonaController : Controller
    {
        public ActionResult FeedBack()
        {
            return base.View();
        }

        public string FormMessage(string name, string phone, string email, string address)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(value: "<html><body style='font-family:calibri;'><p style='font-size:14px;'>Hi ,</p>");
            stringBuilder.Append(value: "<br/><p style='font-size:13px;font-weight:bold;color:rgb(54,101,207)'>CUSTOMER CONTACT DETAIL</p>");
            stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: name.ToUpper(), str2: "</span></li></ul></div>"));
            stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: phone.ToUpper(), str2: "</span></li></ul></div>"));
            stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: email.ToUpper(), str2: "</span></li></ul></div>"));
            stringBuilder.Append(string.Concat(str0: "<ul><li><span style='color:rgb(196,0,0);'>", str1: address.ToUpper(), str2: "</span></li></ul></div>"));
            stringBuilder.Append(value: "<br/><br/><hr/>");
            stringBuilder.Append(value: "<p>For any information please <a style='color:gray' href='http://tikona.bsolution.in'>Contact us</a>.</p>");
            stringBuilder.Append(value: "<br/><br/>");
            stringBuilder.Append(value: "<p>Call us:<span style='color:gray'> 044 66072734</span> </p>");
            stringBuilder.Append(value: "</body></html>");
            return stringBuilder.ToString();
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult Register()
        {
            return base.View();
        }

        public ActionResult Submit(string userName, string emailAddress, string phone, string address)
        {
            var mailMessage = new MailMessage(from: "info@bsolution.in", to: "info@bsolution.in")
            {
                Subject = "REGISTRATION CONFIRMATION FOR TIKONA BROADBAND",
                Body = this.FormMessage(userName, phone, emailAddress, address),
                IsBodyHtml = true
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
            this.PushGcm(userName, emailAddress, phone, address);
            return base.View();
        }

        private void PushGcm(string userName, string emailAddress, string phone, string address)
        {
            var ids = new List<string>();
            ids.Add(item: "APA91bFEuMlP090Q87VN7XV8XcLioomLgwPXjjOiwFJu0n6n_D9TPLhDcmvRUBZKZoQHftkMWRI5oz0td3ubZNMSyZY31F8H0zChOK1E8T7mHQzHNt6XrRFsyBCSIh7Rpevhr_jSmjGRYpY4D1bsHQqeZVbPQcZOPw");
            ids.Add(item: "APA91bGzLB_loCzcVoLQQbXm8pqh6uDlo4Ls-spAAe7PuKoJ24c7HjlbruMHAwIC8i6uTscsEaXkA2QxyxesF0OUYk-2xkCAjV8G1LBYpjoAxwBc1c48gscGe7cHV13n3ITklZPsysONzQDKX_ICU5t51_fFp5O_ww");
            ids.Add(item: "APA91bEur5wp6eu_MNaaf6NCcUtcIFauCgX1Ve7z46DLlwfw5se45KmkcxjWjsHp3ZaRdgcCZv11ZdSnGRmSikbliRGgR4Ojfxl062Wg7jl38ZFqFUqVeRRI1rPWEzRxDngXqwogdw29DPkwVb2xu4j0dEtmDRf6mQ");

            WebRequest tRequest;
            tRequest = WebRequest.Create(requestUriString: "https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format(format: "Authorization: key={0}", arg0: "AIzaSyD-gb5AN6i-FKLTFLKXckqc5PIiTLe9cbs"));

            string postData = new JavaScriptSerializer().Serialize(new
            {
                data = new
                {
                    data1 = userName,
                    data2 = address + " - " + phone
                },
                registration_ids = ids
            });
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, offset: 0, count: byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            var tReader = new StreamReader(dataStream);

            string sResponseFromServer = tReader.ReadToEnd();
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }
    }
}