using System.Configuration;

namespace Bns.Framework.Common.Messaging
{
    public class EmailRiderSettings : ConfigurationSection
    {
        [ConfigurationProperty("From", DefaultValue = "", IsRequired = false)]
        public string From
        {
            get { return (string)this["From"]; }
            set { this["From"] = value; }
        }

        [ConfigurationProperty("To", DefaultValue = "", IsRequired = true)]
        public string To
        {
            get { return (string)this["To"]; }
            set { this["To"] = value; }
        }

        [ConfigurationProperty("Bcc", DefaultValue = "", IsRequired = false)]
        public string Bcc
        {
            get { return (string)this["Bcc"]; }
            set { this["Bcc"] = value; }
        }

        [ConfigurationProperty("Cc", DefaultValue = "", IsRequired = false)]
        public string Cc
        {
            get { return (string)this["Cc"]; }
            set { this["Cc"] = value; }
        }

        [ConfigurationProperty("Host", DefaultValue = "", IsRequired = false)]
        public string Host
        {
            get { return (string)this["Host"]; }
            set { this["Host"] = value; }
        }

        

        [ConfigurationProperty("Port", DefaultValue = "143", IsRequired = false)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }

        [ConfigurationProperty("UserName", DefaultValue = "", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["UserName"]; }
            set { this["UserName"] = value; }
        }

        [ConfigurationProperty("Password", DefaultValue = "", IsRequired = false)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
    }
}