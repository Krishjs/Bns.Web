using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bns.Framework.Common.Messaging
{
    public class SettingsManager
    {
        public static EmailRiderSettings EmailRiderSettings
        {
            get
            {
                return (EmailRiderSettings)System.Configuration.ConfigurationManager.GetSection("MailSettings/EmailRiderSettings");
            }
        }

        public static MailReaderSettings MailReaderSettings
        {
            get
            {
                return (MailReaderSettings)System.Configuration.ConfigurationManager.GetSection("MailReader/ReaderSettings");
            }
        }

        public static MailReceiverSettings MailReceiverSettings
        {
            get
            {
                return (MailReceiverSettings)System.Configuration.ConfigurationManager.GetSection("MailSettings/MailReceiverSettings");
            }
        }
    }
}