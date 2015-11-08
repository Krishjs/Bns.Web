using System;
using System.Configuration;
using System.Globalization;

namespace Bns.Framework.Common.Messaging
{
    public class MailReaderSettings : ConfigurationSection
    {
        private string dateFormat = "dd-MMM-yyyy HH:mm:ss";

        [ConfigurationProperty("AutoStart", DefaultValue = "false", IsRequired = true)]
        public bool AutoStart
        {
            get { return (bool)this["AutoStart"]; }
            set { this["AutoStart"] = value; }
        }

        [ConfigurationProperty("Interval", DefaultValue = "60", IsRequired = true)]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }

        [ConfigurationProperty("TimeStamp")]
        public DateTime TimeStamp
        {
            get
            {
                if ((DateTime)this["TimeStamp"] == default(DateTime))
                {
                    this["TimeStamp"] = DateTime.Now;
                }
                return (DateTime)this["TimeStamp"];
            }
            set
            {
                this["TimeStamp"] = value;
            }
        }
    }
}