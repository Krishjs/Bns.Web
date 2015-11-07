using Bns.Framework.Common.Errors;
using Bns.Framework.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Tikona.BSolution.Controllers
{
    public class AdminController : Controller
    {
        public CancellationTokenSource canceltoken = new CancellationTokenSource();

        public ActionResult Login()
        {
            ViewData["Message"] = TempData["Message"];
            return View();
        }

        public ActionResult ShowErrors(string tkname, string tkpass)
        {
            if (string.IsNullOrEmpty(tkpass))
                return View();
            if (IsUserValid(tkpass))
            {
                ViewData["Message"] = ErrorQueue.GetErrors();
            }
            else
            {
                ViewData["Message"] = "Enter Credentials";
            }
            return View();
        }

        // GET: Admin
        public ActionResult Service(string tkname, string tkpass, DateTime? tkdate = null, bool start = false, bool stop = true, int tkinterval = 10)
        {
            if (IsUserValid(tkpass))
            {                
                if (start)
                {
                    MailReader.SetTimeStamp(tkdate.HasValue ? tkdate.Value : DateTime.Now);
                    MailReader.SetInterval(tkinterval);
                    MailReader.SetPath(HttpContext.Server.MapPath(path: "~/Pdf/"));
                    MailReader.StartTask();
                }
                else
                {
                    MailReader.StopTask();
                }
                TempData["Message"] = "Mail Services " + (start ? "Started!" : "Stopped");
            }
            else
            {
                TempData["Message"] = "In correct Password";
            }
            return RedirectToAction(actionName: "Login");
        }

        private bool IsUserValid(string password)
        {
            string salt = "+vRIgBmV5Pv24S+74R6Pog==";
            string hash = "UHA2nUdLn0YOrdNh4fc5MA==";
            int iteration = 1989;
            var hasher = new Rfc2898DeriveBytes(password, Encoding.Default.GetBytes(salt), iteration);
            string lhs = Convert.ToBase64String(hasher.GetBytes(cb: 16));
            return lhs == hash;
        }
    }
}