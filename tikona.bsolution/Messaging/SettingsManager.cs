using Bns.Framework.Common.ExcelConvertion;
using Bns.Framework.Common.Extraction;
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

        public static ExcelOutputSettings ExcelOutputSettings
        {
            get
            {
                return (ExcelOutputSettings)System.Configuration.ConfigurationManager.GetSection("ExcelOutput/ExcelOutputSettings");
            }
        }

        public static ExtractionSettings ExtractionSettings
        {
            get
            {
                return (ExtractionSettings)System.Configuration.ConfigurationManager.GetSection("Extraction/ExtractionSettings");
            }
        }
    }
}