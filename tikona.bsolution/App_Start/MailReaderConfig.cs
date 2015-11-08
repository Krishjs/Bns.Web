using Bns.Framework.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tikona.Bsolution
{
    public class MailReaderConfig
    {
        public static void RegisterMailReader(List<IRider> riders)
        {
            MailReader.Init(HttpContext.Current.Server.MapPath(path: "~/Pdf/"));
            riders.Add(new EmailRider());
        }
    }
}