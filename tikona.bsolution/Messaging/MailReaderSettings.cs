using System;
using System.Configuration;
using System.Globalization;

namespace Bns.Framework.Common.Messaging
{
    public class MailReaderSettings : ConfigurationSection
    {
        private DateTime? date = null;

        private DateTime GetDate()
        {
            if (!date.HasValue)
                date = DateTime.Now.AddMinutes(-Interval);
            return date.Value;
        }

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
                    return GetDate();
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